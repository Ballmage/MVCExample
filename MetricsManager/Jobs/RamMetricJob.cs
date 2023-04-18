using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.Clients;
using MetricsManager.Models;
using MetricsManager.Repositories;
using MetricsManager.Repositories.Interfaces;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<RamMetricJob> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IMapper _mapper;
        public RamMetricJob(IRamMetricsRepository metricsRepository, ILogger<RamMetricJob> logger,
            IMetricsAgentClient metricsAgentClient, IMapper mapper, IAgentsRepository agentsRepository)
        {
            _metricsRepository = metricsRepository;
            _agentsRepository = agentsRepository;
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("starting new request to metrics agent");

            var agents = _agentsRepository.GetAll();
            if (agents.Any())
            {
                foreach (var agent in agents)
                {
                    try
                    {
                        var metrics = _metricsAgentClient.GetAllRamMetrics(new GetAllRamMetricsApiRequest
                        {
                            AgentUrl = agent.AgentUrl,
                            FromTime = _metricsRepository.GetLastRecordTimeByAgentId(agent.AgentId),
                            ToTime = DateTimeOffset.UtcNow
                        });
                        var metricForManagerDb = new List<RamMetric>();
                        foreach (var metric in metrics.Metrics)
                        {
                            metricForManagerDb.Add(_mapper.Map<RamMetric>(metric, id => metric.AgentID = agent.AgentId));
                        }
                        _metricsRepository.Create(metricForManagerDb);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}

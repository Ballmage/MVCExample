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
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<DotNetMetricJob> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IMapper _mapper;
        public DotNetMetricJob(IDotNetMetricsRepository metricsRepository, ILogger<DotNetMetricJob> logger,
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
                        var metrics = _metricsAgentClient.GetAllDotNetMetrics(new GetAllDotNetMetricsApiRequest
                        {
                            AgentUrl = agent.AgentUrl,
                            FromTime = _metricsRepository.GetLastRecordTimeByAgentId(agent.AgentId),
                            ToTime = DateTimeOffset.UtcNow
                        });
                        var metricForManagerDb = new List<DotNetMetric>();
                        foreach (var metric in metrics.Metrics)
                        {
                            metricForManagerDb.Add(_mapper.Map<DotNetMetric>(metric, id => metric.AgentID = agent.AgentId));
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

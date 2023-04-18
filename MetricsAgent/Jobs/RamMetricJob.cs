using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IRamMetricsRepository _repository;
        private readonly PerformanceCounter _ramCounter;
        public RamMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            var scope = _provider.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<IRamMetricsRepository>();
            // _repository = _provider.GetService<IRamMetricsRepository>();
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        }
        public Task Execute(IJobExecutionContext context)
        {
            var availableMemory = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.RamMetric { Time = time, Value = availableMemory });
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;
        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            var scope = _provider.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<IHddMetricsRepository>();
            //   _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var hddUsage = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.HddMetric { Time = time, Value = hddUsage });
            return Task.CompletedTask;
        }
    }
}

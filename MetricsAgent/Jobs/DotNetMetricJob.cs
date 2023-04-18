using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            var scope = _provider.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<IDotNetMetricsRepository>();
            //    _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all heaps", "_Global_");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allHeapSizeInKBytes = Convert.ToInt32(_dotNetCounter.NextValue() / 1024);
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.DotNetMetric { Time = time, Value = allHeapSizeInKBytes });
            return Task.CompletedTask;
        }
    }
}

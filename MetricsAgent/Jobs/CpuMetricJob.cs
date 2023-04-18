using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly ICpuMetricsRepository _repository;
        // счетчик для метрики CPU
        private readonly PerformanceCounter _cpuCounter;
        public CpuMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            //  _repository = _provider.GetService<ICpuMetricsRepository>();
            var scope = _provider.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<ICpuMetricsRepository>();
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.CpuMetric { Time = time, Value = cpuUsageInPercents });
            return Task.CompletedTask;
        }
    }
}

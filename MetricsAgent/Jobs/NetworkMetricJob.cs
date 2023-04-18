using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter[] _networkCounters;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            var scope = _provider.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<INetworkMetricsRepository>();
            //  _repository = _provider.GetService<INetworkMetricsRepository>();
            var category = new PerformanceCounterCategory("Network Interface");
            string[] instanceNames = category.GetInstanceNames();
            _networkCounters = new PerformanceCounter[instanceNames.Length];
            int count = 0;
            foreach (var instance in instanceNames)
            {
                _networkCounters[count] = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
                count++;
            }
        }
        public Task Execute(IJobExecutionContext context)
        {

            int totalBytesReceived = 0;
            foreach (var item in _networkCounters)
            {
                totalBytesReceived += Convert.ToInt32(item.NextValue());
            }
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.NetworkMetric { Time = time, Value = totalBytesReceived });
            return Task.CompletedTask;
        }
    }
}

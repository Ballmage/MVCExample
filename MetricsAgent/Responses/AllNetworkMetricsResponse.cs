using System.Collections.Generic;
using MetricsAgent.DTO;

namespace MetricsAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}

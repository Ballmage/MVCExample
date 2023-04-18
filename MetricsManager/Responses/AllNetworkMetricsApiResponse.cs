using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }

    public class SelectByTimePeriodNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}

using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }

    public class SelectByTimePeriodRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}

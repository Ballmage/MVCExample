using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }

    public class SelectByTimePeriodHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}

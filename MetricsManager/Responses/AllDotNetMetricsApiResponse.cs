using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class SelectByTimePeriodDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}

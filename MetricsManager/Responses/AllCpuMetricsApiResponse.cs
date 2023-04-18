using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class SelectByTimePeriodCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}

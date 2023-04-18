using System.Collections.Generic;
using MetricsAgent.DTO;

namespace MetricsAgent.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}

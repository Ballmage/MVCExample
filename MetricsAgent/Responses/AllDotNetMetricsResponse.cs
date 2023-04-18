using System.Collections.Generic;
using MetricsAgent.DTO;

namespace MetricsAgent.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}

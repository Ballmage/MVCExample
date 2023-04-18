using System;

namespace MetricsManager.Requests
{
    public class GetAllCpuMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}

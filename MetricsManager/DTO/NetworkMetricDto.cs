using System;

namespace MetricsManager.DTO
{
    public class NetworkMetricDto
    {
        public int Id { get; set; }
        public int AgentID { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}

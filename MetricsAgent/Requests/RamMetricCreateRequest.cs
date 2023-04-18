using System;

namespace MetricsAgent.Requests
{
    public class RamMetricCreateRequest
    {
        private DateTimeOffset time { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time
        {
            get => time;
            set => time = new DateTimeOffset(value.DateTime, TimeSpan.FromHours(0));
        }
    }
}

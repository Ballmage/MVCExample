namespace MetricsCommon.SQL_Settings
{
    public interface ISqlSettings
    {
        public string this[Tables key] { get; }
        public string this[AgentFields key] { get; }
        public string this[ManagerFields key] { get; }
        public string this[RegisteredAgentsFields key] { get; }
    }
}

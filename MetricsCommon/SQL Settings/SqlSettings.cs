using System.Collections.Generic;

namespace MetricsCommon.SQL_Settings
{
    public enum Tables
    {
        CpuMetrics,
        DotNetMetrics,
        HddMetrics,
        NetworkMetrics,
        RamMetrics
    }
    public enum AgentFields
    {
        Id,
        Value,
        Time
    }
    public enum ManagerFields
    {
        Id,
        AgentId,
        Value,
        Time
    }
    public enum RegisteredAgentsFields
    {
        AgentId,
        AgentUrl
    }

    public class SqlSettings : ISqlSettings
    {
        private static readonly string _connectionString = @"Data Source=metrics.db; Version=3;";
        private static readonly string _agentsTable = "registeredagents";

        private readonly Dictionary<Tables, string> _tablesDb = new()
        {
            { Tables.CpuMetrics, "cpumetrics" },
            { Tables.DotNetMetrics, "dotnetmetrics" },
            { Tables.HddMetrics, "hddmetrics" },
            { Tables.NetworkMetrics, "networkmetrics" },
            { Tables.RamMetrics, "rammetrics" },
        };
        private readonly Dictionary<AgentFields, string> _agentFields = new()
        {
            { AgentFields.Id, "Id" },
            { AgentFields.Time, "Time" },
            { AgentFields.Value, "Value" },
        };
        private readonly Dictionary<ManagerFields, string> _managerFields = new()
        {
            { ManagerFields.Id, "Id" },
            { ManagerFields.AgentId, "AgentId" },
            { ManagerFields.Time, "Time" },
            { ManagerFields.Value, "Value" },
        };
        private readonly Dictionary<RegisteredAgentsFields, string> _registeredAgentsFields = new()
        {
            { RegisteredAgentsFields.AgentId, "AgentId" },
            { RegisteredAgentsFields.AgentUrl, "AgentUrl" },
        };

        public static string ConnectionString => _connectionString;

        public static string AgentsTable => _agentsTable;

        public string this[Tables key] => _tablesDb[key];

        public string this[AgentFields key] => _agentFields[key];

        public string this[ManagerFields key] => _managerFields[key];

        public string this[RegisteredAgentsFields key] => _registeredAgentsFields[key];
    }
}

using FluentMigrator;
using System;
using MetricsCommon.SQL_Settings;

namespace MetricsManager.Settings.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        private readonly ISqlSettings _settingsSql;
        public FirstMigration(ISqlSettings settingsSql)
        {
            _settingsSql = settingsSql;
        }
        public override void Up()
        {
            foreach (var table in Enum.GetValues(typeof(Tables)))
            {
                Create.Table(_settingsSql[(Tables)table])
                    .WithColumn(_settingsSql[ManagerFields.Id]).AsInt64().PrimaryKey().Identity()
                    .WithColumn(_settingsSql[ManagerFields.AgentId]).AsInt32()
                    .WithColumn(_settingsSql[ManagerFields.Value]).AsInt32()
                    .WithColumn(_settingsSql[ManagerFields.Time]).AsInt64();
            }
            Create.Table(SqlSettings.AgentsTable)
                .WithColumn(_settingsSql[RegisteredAgentsFields.AgentId]).AsInt32().PrimaryKey().Identity()
                .WithColumn(_settingsSql[RegisteredAgentsFields.AgentUrl]).AsString();
        }
        public override void Down()
        {
            foreach (var table in Enum.GetValues(typeof(Tables)))
            {
                Delete.Table(_settingsSql[(Tables)table]);
            }
            Delete.Table(SqlSettings.AgentsTable);
        }
    }
}

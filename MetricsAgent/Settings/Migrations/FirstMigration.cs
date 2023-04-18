using System;
using FluentMigrator;
using MetricsCommon.SQL_Settings;

namespace MetricsAgent.Migrations
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
                    .WithColumn(_settingsSql[AgentFields.Id]).AsInt64().PrimaryKey().Identity()
                    .WithColumn(_settingsSql[AgentFields.Value]).AsInt32()
                    .WithColumn(_settingsSql[AgentFields.Time]).AsInt64();
            }
        }
        public override void Down()
        {
            foreach (var table in Enum.GetValues(typeof(Tables)))
            {
                Delete.Table(_settingsSql[(Tables)table]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsCommon.SQL_Settings;
using MetricsManager.Models;
using MetricsManager.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly ILogger<AgentsRepository> _logger;
        public AgentsRepository(ILogger<AgentsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(AgentInfo item)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                connection.Execute($"INSERT INTO {SqlSettings.AgentsTable}({RegisteredAgentsFields.AgentId}," +
                                   $" {RegisteredAgentsFields.AgentUrl}) VALUES(@AgentId, @AgentUrl)",
                    new { AgentId = item.AgentId, AgentUrl = item.AgentUrl });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void Delete(int agentId)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                connection.Execute($"DELETE FROM {SqlSettings.AgentsTable} WHERE ({RegisteredAgentsFields.AgentId} = {agentId})");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public IList<AgentInfo> GetAll()
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                return connection.Query<AgentInfo>($"SELECT * FROM {SqlSettings.AgentsTable}").ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public AgentInfo GetAgentById(int agentId)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                return connection.QueryFirstOrDefault<AgentInfo>($"SELECT * FROM {SqlSettings.AgentsTable} " +
                                                                    $"WHERE ({RegisteredAgentsFields.AgentId} = {agentId})");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public string FindUrl(string url)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                return connection.QueryFirstOrDefault<string>($"SELECT {RegisteredAgentsFields.AgentUrl} FROM {SqlSettings.AgentsTable} " +
                                                              $"WHERE ({RegisteredAgentsFields.AgentUrl} = @AgentUrl)",
                    new { AgentUrl = url });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}

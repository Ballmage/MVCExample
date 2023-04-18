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
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly ILogger<DotNetMetricsRepository> _logger;

        public DotNetMetricsRepository(ILogger<DotNetMetricsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(List<DotNetMetric> item)
        {
            using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var metric in item)
                {
                    connection.ExecuteAsync($"INSERT INTO {Tables.DotNetMetrics}" +
                                            $"({ManagerFields.AgentId}, {ManagerFields.Value}, {ManagerFields.Time}) " +
                                            "VALUES(@agentid, @value, @time)",
                        new
                        {
                            agentid = metric.AgentID,
                            value = metric.Value,
                            time = metric.Time
                        });
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                _logger.LogError(e.Message);
            }
        }

        public IList<DotNetMetric> GetByTimePeriodByAgentId(int requestedAgent, long getFromTime, long getToTime)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                return connection.Query<DotNetMetric>($"SELECT * FROM {Tables.DotNetMetrics} " +
                                                   $"WHERE ({ManagerFields.AgentId} = @agentId) " +
                                                   $"AND ({ManagerFields.Time} >= @fromTime) AND ({ManagerFields.Time} <= @toTime)",
                    new { agentId = requestedAgent, fromTime = getFromTime, toTime = getToTime }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public IList<DotNetMetric> GetByTimePeriodFromAllAgents(long getFromTime, long getToTime)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                return connection.Query<DotNetMetric>($"SELECT * FROM {Tables.DotNetMetrics} " +
                                                   $"WHERE ({ManagerFields.Time} >= @fromTime) AND ({ManagerFields.Time} <= @toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public DateTimeOffset GetLastRecordTimeByAgentId(int agentId)
        {
            try
            {
                using var connection = new SQLiteConnection(SqlSettings.ConnectionString);
                var result = connection.QuerySingleOrDefault<long>($"SELECT MAX({ManagerFields.Time}) " +
                                                                   $"FROM {Tables.DotNetMetrics} " + $"WHERE ({ManagerFields.AgentId} = {agentId})");
                return DateTimeOffset.FromUnixTimeSeconds(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return DateTimeOffset.FromUnixTimeSeconds(0);
        }
    }
}

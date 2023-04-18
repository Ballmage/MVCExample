using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
        
        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<NetworkMetric>("SELECT id, time, value FROM networkmetrics").ToList();
            }
        }
        
        public IList<NetworkMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}

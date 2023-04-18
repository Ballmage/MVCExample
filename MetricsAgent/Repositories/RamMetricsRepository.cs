using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public RamMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
        
        public IList<RamMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<RamMetric>("SELECT id, time, value FROM rammetrics").ToList();
            }
        }
        
        public IList<RamMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}

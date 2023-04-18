using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly SQLiteConnection _connection;
        
        public DotNetMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
        
        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<DotNetMetric>("SELECT id, time, value FROM dotnetmetrics").ToList();
            }
        }
        
        public IList<DotNetMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}

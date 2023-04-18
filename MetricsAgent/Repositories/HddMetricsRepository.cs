using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
        
        public IList<HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<HddMetric>("SELECT id, time, value FROM hddmetrics").ToList();
            }
        }
        
        public IList<HddMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}

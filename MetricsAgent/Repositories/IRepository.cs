using System.Collections.Generic;

namespace MetricsAgent.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        void Create(T item);
        IList<T> GetByTimePeriod(long fromTime, long toTime);
    }
}

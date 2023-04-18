using System;
using System.Collections.Generic;

namespace MetricsManager.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(List<T> item);
        IList<T> GetByTimePeriodByAgentId(int agentId, long fromTime, long toTime);
        IList<T> GetByTimePeriodFromAllAgents(long fromTime, long toTime);
        DateTimeOffset GetLastRecordTimeByAgentId(int agentId);
    }
}

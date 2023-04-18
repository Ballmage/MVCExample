using System.Collections.Generic;
using MetricsManager.Models;

namespace MetricsManager.Repositories.Interfaces
{
    public interface IAgentsRepository
    {
        public void Create(AgentInfo item);
        public void Delete(int agentId);
        public IList<AgentInfo> GetAll();
        public AgentInfo GetAgentById(int agentId);
        public string FindUrl(string url);
    }
}

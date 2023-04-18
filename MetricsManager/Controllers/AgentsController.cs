using Microsoft.AspNetCore.Mvc;
using MetricsManager.Models;
using MetricsManager.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(IAgentsRepository agentsRepository, ILogger<AgentsController> logger)
        {
            _agentsRepository = agentsRepository;
            _logger = logger;
            _logger.LogDebug(1, "AgentController created");
        }


        private bool IsAgentRegistered(AgentInfo agent)
        {
            return _agentsRepository.FindUrl(agent.AgentUrl) != null;
        }

        /// <summary>
        /// Регистрирует нового агента
        /// </summary>
        /// <param name="agentInfo">Информация об агенте</param>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogTrace($"Agent registered with params: AgentID={agentInfo.AgentId}, AgentAddress={agentInfo.AgentUrl}");

            if (!IsAgentRegistered(agentInfo))
            {
                _agentsRepository.Create(agentInfo);
                return Ok();
            }

            return BadRequest("Агент с таким URL уже зарегистрирован!");
        }

        /// <summary>
        /// Удаляет агента по указанному ID
        /// </summary> 
        [HttpDelete("unregister/{agentId}")]
        public IActionResult DeleteAgent([FromRoute] int agentId)
        {
            _logger.LogTrace($"Agent unregistered: AgentID={agentId}");

            _agentsRepository.Delete(agentId);
            return Ok();
        }

        /// <summary>
        /// Считывает зарегистрированных агентов
        /// </summary>
        [HttpGet("read")]
        public IActionResult ListOfRegisteredObjects()
        {
            _logger.LogTrace("Query for all registered agents");

            var response = _agentsRepository.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Возвращает информацию об агенте по его Id
        /// </summary>
        [HttpGet("getById/{agentId}")]
        public IActionResult GetAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"Query for agent info for Agent: {agentId}");

            var response = _agentsRepository.GetAgentById(agentId);
            return Ok(response);
        }
    }
}

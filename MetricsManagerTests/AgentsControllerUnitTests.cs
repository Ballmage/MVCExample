using MetricsManager.Controllers;
using System.Collections.Generic;
using AutoFixture;
using MetricsManager.Models;
using MetricsManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController _controller;
        private readonly List<AgentInfo> _agents;
        private readonly Mock<IAgentsRepository> _mockRepository;

        public AgentsControllerUnitTests()
        {
            var mockLogger = new Mock<ILogger<AgentsController>>();
            _mockRepository = new Mock<IAgentsRepository>();
            _agents = new Fixture().Create<List<AgentInfo>>();
            _controller = new AgentsController(_mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void RegisterAgent_ShouldCall_Create_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<AgentInfo>())).Verifiable();
            var result = _controller.RegisterAgent(_agents[0]);
            _mockRepository.Verify(repository => repository.Create(It.IsAny<AgentInfo>()), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DeleteAgent_ShouldCall_DeleteAgent_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Delete(It.IsAny<int>())).Verifiable();
            var result = _controller.DeleteAgent(_agents[0].AgentId);
            _mockRepository.Verify(repository => repository.Delete(It.IsAny<int>()), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetAllAgents_ShouldCall_GetAll_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAll()).Returns(_agents).Verifiable();
            var result = (OkObjectResult)_controller.ListOfRegisteredObjects();
            var actualResult = (List<AgentInfo>)result.Value;
            _mockRepository.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
            for (int i = 0; i < _agents.Count; i++)
            {
                Assert.Equal(_agents[i].AgentId, actualResult[i].AgentId);
                Assert.Equal(_agents[i].AgentUrl, actualResult[i].AgentUrl);
            }
        }

        [Fact]
        public void GetAgentById_ShouldCall_GetAgentById_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAgentById(It.IsAny<int>()))
                .Returns(_agents[0]).Verifiable();
            var result = (OkObjectResult)_controller.GetAgentById(It.IsAny<int>());
            var actualResult = (AgentInfo)result.Value;
            _mockRepository.Verify(repository => repository.GetAgentById(It.IsAny<int>()), Times.AtLeastOnce());
            Assert.Equal(_agents[0].AgentId, actualResult.AgentId);
            Assert.Equal(_agents[0].AgentUrl, actualResult.AgentUrl);
        }
    }
}

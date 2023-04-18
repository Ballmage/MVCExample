using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMapper;
using MetricsCommon;
using MetricsManager.Controllers;
using MetricsManager.Models;
using MetricsManager.Repositories;
using MetricsManager.Responses;
using MetricsManager.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        private readonly Mock<IRamMetricsRepository> _mockRepository;
        private readonly Mock<ILogger<RamMetricsController>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly List<RamMetric> _initialData;
        private readonly Percentile _percentile = Percentile.P99;

        public RamControllerUnitTests()
        {
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mapper = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile())).CreateMapper();
            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object, _mapper);
            _initialData = new Fixture().Create<List<RamMetric>>();
        }

        [Fact]
        public void GetMetricsFromAgent_ShouldCall_GetByTimePeriodByAgentId_From_Repository()
        {
            _mockRepository.Setup(repository => repository
                    .GetByTimePeriodByAgentId(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_initialData).Verifiable();

            var result = (OkObjectResult)_controller.GetMetricsFromAgent(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>());

            var actualResult = ((SelectByTimePeriodRamMetricsResponse)result.Value).Metrics;

            _mockRepository.Verify(repository => repository.GetByTimePeriodByAgentId(It.IsAny<int>(),
                It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());

            for (int i = 0; i < _initialData.Count; i++)
            {
                Assert.Equal(_initialData[i].Value, actualResult[i].Value);
                Assert.Equal(_initialData[i].Id, actualResult[i].Id);
                Assert.Equal(_initialData[i].Time, actualResult[i].Time.ToUnixTimeSeconds());
            }
        }

        [Fact]
        public void GetMetricsByPercentileFromAgent_ShouldCall_GetByTimePeriodByAgentId_From_Repository()
        {
            _mockRepository.Setup(repository => repository
                    .GetByTimePeriodByAgentId(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_initialData).Verifiable();

            var result = (OkObjectResult)_controller.GetMetricsByPercentileFromAgent(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), _percentile);

            var actualResult = ((int)result.Value);

            _mockRepository.Verify(repository => repository.GetByTimePeriodByAgentId(It.IsAny<int>(),
                It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());

            var orderedMetrics = _initialData.OrderBy(m => m.Value);
            var initialPercentile = orderedMetrics.ElementAt((int)(orderedMetrics.Count() * 0.99)).Value;
            Assert.Equal(initialPercentile, actualResult);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ShouldCall_GetByTimePeriodFromAllAgents_From_Repository()
        {
            _mockRepository.Setup(repository => repository
                    .GetByTimePeriodFromAllAgents(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_initialData).Verifiable();

            var result = (OkObjectResult)_controller.GetMetricsFromAllCluster(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>());

            var actualResult = ((SelectByTimePeriodRamMetricsResponse)result.Value).Metrics;
            _mockRepository.Verify(repository => repository.GetByTimePeriodFromAllAgents(It.IsAny<long>(),
                It.IsAny<long>()), Times.AtMostOnce());

            for (int i = 0; i < _initialData.Count; i++)
            {
                Assert.Equal(_initialData[i].Value, actualResult[i].Value);
                Assert.Equal(_initialData[i].Id, actualResult[i].Id);
                Assert.Equal(_initialData[i].Time, actualResult[i].Time.ToUnixTimeSeconds());
            }
        }

        [Fact]
        public void GetMetricsByPercentileFromAllCluster_ShouldCall_GetByTimePeriodFromAllAgents_From_Repository()
        {
            _mockRepository.Setup(repository => repository
                    .GetByTimePeriodFromAllAgents(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_initialData).Verifiable();

            var result = (OkObjectResult)_controller.GetMetricsByPercentileFromAllCluster(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>(), _percentile);

            var actualResult = ((int)result.Value);

            _mockRepository.Verify(repository => repository.GetByTimePeriodFromAllAgents(It.IsAny<long>(),
                It.IsAny<long>()), Times.AtMostOnce());

            var orderedMetrics = _initialData.OrderBy(m => m.Value);
            var initialPercentile = orderedMetrics.ElementAt((int)(orderedMetrics.Count() * 0.99)).Value;
            Assert.Equal(initialPercentile, actualResult);
        }
    }
}

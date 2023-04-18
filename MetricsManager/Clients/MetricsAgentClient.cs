using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Clients
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/cpumetrics/from/{request.FromTime.UtcDateTime:O}/to/" +
                $"{request.ToTime.UtcDateTime:O}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/dotnetmetrics/from/{request.FromTime.UtcDateTime:O}/to/" +
                $"{request.ToTime.UtcDateTime:O}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/hddmetrics/from/{request.FromTime.UtcDateTime:O}/to/" +
                $"{request.ToTime.UtcDateTime:O}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/networkmetrics/from/{request.FromTime.UtcDateTime:O}/to/" +
                $"{request.ToTime.UtcDateTime:O}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/rammetrics/from/{request.FromTime.UtcDateTime:O}/to/" +
                $"{request.ToTime.UtcDateTime:O}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
    }
}

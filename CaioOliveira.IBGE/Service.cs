using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CaioOliveira.IBGE.Configuration;
using CaioOliveira.IBGE.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CaioOliveira.IBGE
{
    public class Service : Interfaces.IIbgeService
    {
        private readonly ILogger _logger;
        private readonly ServiceConfiguration _configuration;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        private readonly HttpClientHandler _handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        public Service(
            ILogger<Service> logger,
            IOptions<ServiceConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration?.Value;

            if (_configuration == null)
                throw new ArgumentException("Configuração inválida");
        }

        public async Task<City> GetCity(int id)
        {
            _logger.LogDebug($"Iniciando pesquisa de cidade { id } no serviço do IBGE");

            try
            {
                using (var client = new HttpClient(_handler))
                {
                    client.BaseAddress = new Uri(_configuration.BaseApiUrl);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var rq = new HttpRequestMessage(HttpMethod.Get, $"localidades/municipios/{id}");
                    var rs = await client.SendAsync(rq);

                    var content = await rs.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Resultado da consulta: { content }");
                    
                    var result = JsonConvert.DeserializeObject<City>(content, _jsonSettings);

                    if (!(result?.Id ?? 0).Equals(0)) return result;
                    
                    _logger.LogWarning($"Cidade { id } não encontrada");
                    return null;
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Erro ao recuerar a cidade");
                return null;
            }
        }

        public async Task<UF> GetState(int id)
        {
            _logger.LogDebug($"Iniciando pesquisa de estado { id } no serviço do IBGE");

            try
            {
                using (var client = new HttpClient(_handler))
                {
                    client.BaseAddress = new Uri(_configuration.BaseApiUrl);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogDebug("Realizando chamada para API do IBGE");
                    var rq = new HttpRequestMessage(HttpMethod.Get, $"localidades/estados/{id}");
                    var rs = await client.SendAsync(rq);

                    var content = await rs.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Resultado da consulta: { content }");
                    
                    var result = JsonConvert.DeserializeObject<UF>(content, _jsonSettings);

                    if (!(result?.Id ?? 0).Equals(0)) return result;
                    
                    _logger.LogWarning($"Estado { id } não encontrada");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar estado");
                return null;
            }
        }

        public async Task<List<UF>> GetStates() 
        {
            _logger.LogDebug($"Recuperando todos os estados no serviço do IBGE");

            try
            {
                using (var client = new HttpClient(_handler))
                {
                    client.BaseAddress = new Uri(_configuration.BaseApiUrl);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogDebug("Realizando chamada para API do IBGE");
                    var rq = new HttpRequestMessage(HttpMethod.Get, $"localidades/estados");
                    var rs = await client.SendAsync(rq);
                    
                    var content = await rs.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Resultado da consulta: { content }");
                    
                    var result = JsonConvert.DeserializeObject<List<UF>>(content, _jsonSettings);

                    if (!(result?.Any() ?? false))
                        _logger.LogWarning($"Nenhum estado retornado pelo serviço do IBGE");

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar os estados na API do IBGE");
                return null;
            }
        }

        public async Task<List<City>> GetCitiesByState(int stateId) 
        {
            _logger.LogError($"Recuperando todos as cidades do estado { stateId } no serviço do IBGE");

            try
            {
                using (var client = new HttpClient(_handler))
                {
                    client.BaseAddress = new Uri(_configuration.BaseApiUrl);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogDebug("Realizando chamada para API do IBGE");
                    var rq = new HttpRequestMessage(HttpMethod.Get, $"localidades/estados/{stateId}/municipios");
                    var rs = await client.SendAsync(rq);

                    var content = await rs.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Resultado da consulta: { content }");
                    
                    var result = JsonConvert.DeserializeObject<List<City>>(content, _jsonSettings);

                    if (!(result?.Any() ?? false))
                        _logger.LogWarning($"Nenhuma cidade para o estado { stateId } retornada pelo serviço do IBGE");

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar lista de cidades na API do IBGE");
                return null;
            }
        }
    }
}

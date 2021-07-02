using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using CaioOliveira.IBGE;
using CaioOliveira.IBGE.Configuration;
using CaioOliveira.IBGE.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CaioOliveira.IBGE.Test
{
    [TestClass]
    public class IbgeServiceTest
    {
        private readonly IIbgeService _service;

        public IbgeServiceTest()
        {
            var loggerMock = new Mock<ILogger<Service>>();
            var configurationMock = new Mock<IOptions<ServiceConfiguration>>();
            var config = new ServiceConfiguration
            {
                BaseApiUrl = "https://servicodados.ibge.gov.br/api/v1/"
            };

            configurationMock.Setup(x => x.Value).Returns(config);
            
            this._service = new Service(loggerMock.Object, configurationMock.Object);
        }


        [TestMethod]
        public async Task GetCity()
        {
            var id = 3550308;

            var city = await this._service.GetCity(id);

            Assert.IsNotNull(city, "A cidade não pode ser null");
            Assert.AreEqual("São Paulo", city.Name, "A cidade retornada não é a esperada");
        }

        [TestMethod]
        public async Task CityNotFound()
        {
            var id = 0;

            var city = await this._service.GetCity(id);

            Assert.IsNull(city, "Nenhuma cidade deveria ser retornada, a entidade deveria ser null");
        }

        [TestMethod]
        public async Task GetState()
        {
            var id = 35;

            var state = await this._service.GetState(id);

            Assert.IsNotNull(state, "O estado n�o pode ser null");
            Assert.AreEqual("SP", state.Initials, "A UF retonada não é a esperada");
            Assert.AreEqual("São Paulo", state.Name, "O nome do estado retornado não é o esperado");
        }

        [TestMethod]
        public async Task StateNotFound()
        {
            var id = 0;

            var state = await this._service.GetState(id);

            Assert.IsNull(state, "Nenhum estado deveria ser retornado, a entidade deveria ser null");
        }

        [TestMethod]
        public async Task GetAllStates() 
        {
            var states = await this._service.GetStates();

            Assert.IsNotNull(states, "O retorno n�o pode ser null");
            Assert.IsTrue(states.Count > 0, "Nenhum estado retornado");
        }

        [TestMethod]
        public async Task GetAllCitiesByState35()
        {
            var cities = await this._service.GetCitiesByState(35);

            Assert.IsNotNull(cities, "O retorno n�o pode ser null");
            Assert.IsTrue(cities.Count > 0, "Nenhuma cidade retornada");
        }
    }
}

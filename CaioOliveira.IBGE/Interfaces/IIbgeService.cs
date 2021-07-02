using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaioOliveira.IBGE.Interfaces
{
    public interface IIbgeService
    {
        Task<Models.City> GetCity(int id);
        Task<Models.UF> GetState(int id);
        Task<List<Models.UF>> GetStates();
        Task<List<Models.City>> GetCitiesByState(int stateId);
    }
}

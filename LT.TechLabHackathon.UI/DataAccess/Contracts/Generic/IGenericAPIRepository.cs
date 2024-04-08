using LT.TechLabHackathon.Shared.Helpers;

namespace LT.TechLabHackathon.UI.DataAccess.Contracts.Generic
{
    public interface IGenericAPIRepository<Q, C> 
    {
        Task<ResponseService<Q>> GetByIdAsync(int id);
        Task<ResponseService<IEnumerable<Q>>> GetAllAsync();
        Task<ResponseService<IEnumerable<Q>>> GetAllWithFiltersAsync(Dictionary<string, object> filters);

        Task<ResponseService<Q>> Create(C createNew);
        Task<ResponseService<bool>> Update(C entityUpdated, int id);
    }
}

using Core.Models;

namespace Core.Interfaces
{
    public interface IExpanseRepository
    {
        Task<Expanse> CreateExpanseAsync(Expanse expanse);
        Task DeleteExpanseAsync(string id);
        Task<List<Expanse>> GetAllExpansesAsync();
        Task<Expanse> GetExpanseAsync(string id);
        Task<Expanse> UpdateExpanseAsync(Expanse expanse);
    }
}
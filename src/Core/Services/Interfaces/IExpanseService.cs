using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IExpanseService
    {
        Task<List<Expanse>> GetAllExpansesAsync();
        Task<Expanse> GetExpanseAsync(string id);
        Task<string> CreateExpanseAsync(Expanse expanse);
        Task<Expanse> UpdateExpanseAsync(Expanse expanse, string id);
        Task DeleteExpanseAsync(string id);
    }
}

using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ExpanseRepository : IExpanseRepository
    {
        public Task<Expanse> CreateExpanseAsync(Expanse expanse)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExpanseAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Expanse>> GetAllExpansesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Expanse> GetExpanseAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Expanse> UpdateExpanseAsync(Expanse expanse)
        {
            throw new NotImplementedException();
        }
    }
}

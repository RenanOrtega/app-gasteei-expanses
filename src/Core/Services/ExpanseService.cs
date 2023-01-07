using Core.Interfaces;
using Core.Models;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ExpanseService : IExpanseService
    {
        private readonly IExpanseRepository _expanseRepository;

        public ExpanseService(IExpanseRepository expanseRepository)
        {
            _expanseRepository = expanseRepository;
        }

        public async Task<string> CreateExpanseAsync(Expanse expanse)
        {
            Expanse expanseCreated = await _expanseRepository.CreateExpanseAsync(expanse);
            return expanseCreated.Id;
        }

        public async Task DeleteExpanseAsync(string id)
        {
            await _expanseRepository.DeleteExpanseAsync(id);
        }

        public async Task<List<Expanse>> GetAllExpansesAsync()
        {
            List<Expanse> expanses = await _expanseRepository.GetAllExpansesAsync();
            return expanses;
        }

        public async Task<Expanse> GetExpanseAsync(string id)
        {
            Expanse expanse = await _expanseRepository.GetExpanseAsync(id);
            return expanse;
        }

        public async Task<Expanse> UpdateExpanseAsync(Expanse expanse, string id)
        {
            Expanse expanseInDatabase = await GetExpanseAsync(id);

            if (expanseInDatabase.Id != expanse.Id)
            {
                throw new Exception();
            }

            Expanse expanseUpdated = await _expanseRepository.UpdateExpanseAsync(expanse);
            return expanseUpdated;
        }
    }
}

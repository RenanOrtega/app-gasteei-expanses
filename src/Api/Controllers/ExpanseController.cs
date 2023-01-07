using Core.Models;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ExpanseController : ControllerBase
    {
        private readonly IExpanseService _expanseService;

        public ExpanseController(IExpanseService expanseService)
        {
            _expanseService = expanseService;
        }

        [HttpGet]
        public async Task<List<Expanse>> GetAllExpansesAsync()
        {
            List<Expanse> expanses = await _expanseService.GetAllExpansesAsync();
            return expanses;
        }

        [HttpGet("{id}")]
        public async Task<Expanse> GetExpanseAsync([FromRoute] string id)
        {
            Expanse expanse = await _expanseService.GetExpanseAsync(id);
            return expanse;
        }

        [HttpPost("create")]
        public async Task<string> CreateExpanseAsync([FromBody] Expanse expanse)
        {
            string id = await _expanseService.CreateExpanseAsync(expanse);
            return id;
        }

        [HttpPut("update/{id}")]
        public async Task<Expanse> UpdateExpanseAsync([FromBody] Expanse expanse, [FromRoute] string id)
        {
            Expanse expanseUpdated = await _expanseService.UpdateExpanseAsync(expanse, id);
            return expanseUpdated;
        }

        [HttpDelete("delete")]
        public async Task DeleteExpanseAsync([FromRoute] string id)
        {
            await _expanseService.DeleteExpanseAsync(id);
        }
    }
}

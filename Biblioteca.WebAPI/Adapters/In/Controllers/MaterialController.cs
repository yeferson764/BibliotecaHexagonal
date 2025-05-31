using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.WebAPI.Adapters.In.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialUseCases _useCases;

        public MaterialController(MaterialUseCases useCases)
        {
            _useCases = useCases;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _useCases.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var m = await _useCases.ObtenerPorIdAsync(id);
            return m == null ? NotFound() : Ok(m);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Material m)
            => Ok(await _useCases.CrearAsync(m));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Material m)
            => Ok(await _useCases.ActualizarAsync(id, m));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _useCases.EliminarAsync(id));

        [HttpPatch("{id}/enter-stock")]
        public async Task<IActionResult> AddStock(int id, [FromQuery] int cantidad)
            => Ok(await _useCases.AgregarStockAsync(id, cantidad));
    }
}

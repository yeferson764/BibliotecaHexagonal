using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.WebAPI.Adapters.In.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMaterialController : ControllerBase
    {
        private readonly TipoMaterialUseCases _useCases;

        public TipoMaterialController(TipoMaterialUseCases useCases)
        {
            _useCases = useCases;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _useCases.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tipo = await _useCases.ObtenerPorIdAsync(id);
            return tipo == null ? NotFound() : Ok(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoMaterial tipo)
            => Ok(await _useCases.CrearAsync(tipo));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TipoMaterial tipo)
            => Ok(await _useCases.ActualizarAsync(id, tipo));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _useCases.EliminarAsync(id));
    }
}

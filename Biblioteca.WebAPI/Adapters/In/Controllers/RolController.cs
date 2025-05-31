using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.WebAPI.Adapters.In.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly RolUseCases _useCases;

        public RolController(RolUseCases useCases)
        {
            _useCases = useCases;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _useCases.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rol = await _useCases.ObtenerPorIdAsync(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rol r)
            => Ok(await _useCases.CrearAsync(r));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rol r)
            => Ok(await _useCases.ActualizarAsync(id, r));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _useCases.EliminarAsync(id));
    }
}

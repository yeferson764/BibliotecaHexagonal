using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.WebAPI.Adapters.In.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoUseCases _useCases;

        public PrestamoController(PrestamoUseCases useCases)
        {
            _useCases = useCases;
        }

        [HttpGet("historial-completo")]
        public async Task<IActionResult> GetAll() => Ok(await _useCases.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _useCases.ObtenerPorIdAsync(id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> Activos() => Ok(await _useCases.ObtenerActivosAsync());

        [HttpGet("devueltos")]
        public async Task<IActionResult> Devueltos() => Ok(await _useCases.ObtenerDevueltosAsync());

        [HttpGet("persona/{id}")]
        public async Task<IActionResult> ActivosPorPersona(int id) => Ok(await _useCases.ObtenerPorPersonaAsync(id));

        [HttpPost]
        public async Task<IActionResult> Prestar([FromBody] Prestamo p)
            => Ok(await _useCases.PrestarAsync(p));

        [HttpPost("devolucion")]
        public async Task<IActionResult> Devolver([FromQuery] int prestamoId)
            => Ok(await _useCases.DevolverAsync(prestamoId));
    }
}

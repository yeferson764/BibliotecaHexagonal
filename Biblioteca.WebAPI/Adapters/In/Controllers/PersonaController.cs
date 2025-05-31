using Microsoft.AspNetCore.Mvc;
using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.In;

namespace Biblioteca.WebAPI.Adapters.In.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly PersonaUseCases _useCases;

    public PersonaController(PersonaUseCases useCases)
    {
        _useCases = useCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _useCases.ListarAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _useCases.ObtenerPorIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Persona p)
    {
        var result = await _useCases.CrearAsync(p);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Persona p)
    {
        var result = await _useCases.ActualizarAsync(id, p);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _useCases.EliminarAsync(id);
        return Ok(result);
    }

    [HttpGet("disponibilidad/{id}")]
    public async Task<IActionResult> Disponibilidad(int id)
    {
        var result = await _useCases.VerDisponibilidadAsync(id);
        return Ok(result);
    }
}

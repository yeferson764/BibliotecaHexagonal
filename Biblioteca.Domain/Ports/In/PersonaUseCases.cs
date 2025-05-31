using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;

namespace Biblioteca.Domain.Ports.In
{
    public class PersonaUseCases
    {
        private readonly PersonaService _service;
        private readonly IPersonaRepository _repo;
        private readonly IRolRepository? _rolRepo; // opcional, por si usas lógica de capacidad

        public PersonaUseCases(PersonaService service, IPersonaRepository repo, IRolRepository? rolRepo = null)
        {
            _service = service;
            _repo = repo;
            _rolRepo = rolRepo;
        }

        public Task<List<Persona>> ListarAsync() => _service.ListarAsync();
        public Task<Persona?> ObtenerPorIdAsync(int id) => _service.ObtenerPorIdAsync(id);
        public Task<string> ActualizarAsync(int id, Persona p) => _service.ActualizarAsync(id, p);
        public Task<string> EliminarAsync(int id) => _service.EliminarAsync(id);

        public async Task<string> CrearAsync(Persona persona)
        {
            var error = await _service.ValidarPersonaNuevaAsync(persona);
            if (error != null) return error;

            var existente = await _repo.GetByCedulaAsync(persona.Cedula);
            if (existente != null)
                return "Ya existe una persona con esa cédula.";

            await _repo.AddAsync(persona);
            return "Persona registrada exitosamente.";
        }

        public async Task<string> VerDisponibilidadAsync(int id)
        {
            var persona = await _repo.GetByIdAsync(id);
            if (persona == null)
                return "Persona no encontrada.";

            int capacidad = 0;

            if (_rolRepo != null)
            {
                var rol = await _rolRepo.GetByIdAsync(persona.RolId);
                capacidad = rol?.CapacidadPrestamo ?? 0;
            }

            int prestamosActivos = await _repo.GetActiveLoanCountAsync(id);
            return $"Capacidad restante: {capacidad - prestamosActivos}";
        }

        

    }
}

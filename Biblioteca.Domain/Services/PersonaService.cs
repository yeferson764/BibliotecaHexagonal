using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;

namespace Biblioteca.Domain.Services
{
    public class PersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IRolRepository _rolRepo;


        public PersonaService(IPersonaRepository personaRepository, IRolRepository rolRepo)
        {
            _personaRepository = personaRepository;
            _rolRepo = rolRepo;
        }

        public async Task<List<Persona>> ListarAsync()
        {
            return await _personaRepository.GetAllAsync();
        }

        public async Task<Persona?> ObtenerPorIdAsync(int id)
        {
            return await _personaRepository.GetByIdAsync(id);
        }

        public async Task<string> CrearAsync(Persona persona)
        {
            var existente = await _personaRepository.GetByCedulaAsync(persona.Cedula);
            if (existente != null)
                return "Ya existe una persona con esta cédula.";

            await _personaRepository.AddAsync(persona);
            return "Persona creada exitosamente.";
        }

        public async Task<string> ActualizarAsync(int id, Persona nueva)
        {
            var actual = await _personaRepository.GetByIdAsync(id);
            if (actual == null)
                return "Persona no encontrada.";

            actual.Nombre = nueva.Nombre;
            actual.Cedula = nueva.Cedula;
            actual.RolId = nueva.RolId;

            await _personaRepository.UpdateAsync(actual);
            return "Persona actualizada.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
                return "Persona no encontrada.";

            if (await _personaRepository.HasActiveLoansAsync(id))
                return "No se puede eliminar: tiene préstamos activos.";

            await _personaRepository.DeleteAsync(persona);
            return "Persona eliminada.";
        }
        
        public async Task<string?> ValidarPersonaNuevaAsync(Persona persona)
        {
            var rolExiste = await _rolRepo.GetByIdAsync(persona.RolId);
            if (rolExiste == null)
                return "El rol asignado a la persona no existe.";

            return null;
        }
    }
}

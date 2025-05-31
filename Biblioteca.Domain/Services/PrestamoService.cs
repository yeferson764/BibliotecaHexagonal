using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;

namespace Biblioteca.Domain.Services
{
    public class PrestamoService
    {
        private readonly IRolRepository _rolRepo;
        private readonly IPersonaRepository _personaRepo;
        private readonly IMaterialRepository _materialRepo;

        public PrestamoService(IRolRepository rolRepo, IPersonaRepository personaRepo, IMaterialRepository materialRepo)
        {
            _rolRepo = rolRepo;
            _personaRepo = personaRepo;
            _materialRepo = materialRepo;
        }

        public async Task<string?> ValidarPrestamoAsync(int personaId, int materialId)
        {
            var persona = await _personaRepo.GetByIdAsync(personaId);
            if (persona == null) return "La persona no existe.";

            var material = await _materialRepo.GetByIdAsync(materialId);
            if (material == null) return "El material no existe.";
            if (material.CantidadActual <= 0) return "No hay stock disponible.";

            var rol = await _rolRepo.GetByIdAsync(persona.RolId);
            if (rol == null) return "Rol no encontrado.";

            int activos = await _personaRepo.GetActiveLoanCountAsync(personaId);
            if (activos >= rol.CapacidadPrestamo)
                return $"Capacidad de préstamo alcanzada ({rol.CapacidadPrestamo}).";

            return null;
        }
    }
}

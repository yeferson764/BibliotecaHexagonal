using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;

namespace Biblioteca.Domain.Services
{
    public class RolService
    {
        private readonly IRolRepository _rolRepo;
        private readonly IPersonaRepository _personaRepo;

        public RolService(IRolRepository rolRepo, IPersonaRepository personaRepo)
        {
            _rolRepo = rolRepo;
            _personaRepo = personaRepo;
        }

        public async Task<string?> ValidarNuevoRolAsync(Rol rol)
        {
            if (rol.CapacidadPrestamo <= 0)
                return "La capacidad de préstamo debe ser mayor que cero.";

            var existente = await _rolRepo.GetByNombreAsync(rol.RolName);
            if (existente != null)
                return "Ya existe un rol con ese nombre.";

            return null;
        }

        public async Task<string?> ValidarEliminacionAsync(int rolId)
        {
            bool enUso = await _personaRepo.ExistsByRolIdAsync(rolId);
            return enUso ? "No se puede eliminar: existen personas con este rol." : null;
        }
    }
}

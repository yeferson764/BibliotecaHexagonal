using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;

namespace Biblioteca.Domain.Ports.In
{
    public class RolUseCases
    {
        private readonly IRolRepository _repo;
        private readonly RolService _service;

        public RolUseCases(IRolRepository repo, RolService service)
        {
            _repo = repo;
            _service = service;
        }

        public Task<List<Rol>> ListarAsync() => _repo.GetAllAsync();
        public Task<Rol?> ObtenerPorIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<string> CrearAsync(Rol rol)
        {
            var error = await _service.ValidarNuevoRolAsync(rol);
            if (error != null) return error;

            await _repo.AddAsync(rol);
            return "Rol creado exitosamente.";
        }

        public async Task<string> ActualizarAsync(int id, Rol nuevo)
        {
            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Rol no encontrado.";

            actual.RolName = nuevo.RolName;
            actual.CapacidadPrestamo = nuevo.CapacidadPrestamo;

            await _repo.UpdateAsync(actual);
            return "Rol actualizado.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            var error = await _service.ValidarEliminacionAsync(id);
            if (error != null) return error;

            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Rol no encontrado.";

            await _repo.DeleteAsync(actual);
            return "Rol eliminado.";
        }
    }
}

using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;

namespace Biblioteca.Domain.Ports.In
{
    public class TipoMaterialUseCases
    {
        private readonly ITipoMaterialRepository _repo;
        private readonly TipoMaterialService _service;

        public TipoMaterialUseCases(ITipoMaterialRepository repo, TipoMaterialService service)
        {
            _repo = repo;
            _service = service;
        }

        public Task<List<TipoMaterial>> ListarAsync() => _repo.GetAllAsync();
        public Task<TipoMaterial?> ObtenerPorIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<string> CrearAsync(TipoMaterial tipo)
        {
            var error = await _service.ValidarNuevoAsync(tipo);
            if (error != null) return error;

            await _repo.AddAsync(tipo);
            return "Tipo de material creado exitosamente.";
        }

        public async Task<string> ActualizarAsync(int id, TipoMaterial nuevo)
        {
            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Tipo de material no encontrado.";

            actual.Tipo = nuevo.Tipo;
            await _repo.UpdateAsync(actual);
            return "Tipo de material actualizado.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            var error = await _service.ValidarEliminacionAsync(id);
            if (error != null) return error;

            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Tipo de material no encontrado.";

            await _repo.DeleteAsync(actual);
            return "Tipo de material eliminado.";
        }
    }
}

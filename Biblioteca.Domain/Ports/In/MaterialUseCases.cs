using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;

namespace Biblioteca.Domain.Ports.In
{
    public class MaterialUseCases
    {
        private readonly IMaterialRepository _repo;
        private readonly MaterialService _service;

        public MaterialUseCases(IMaterialRepository repo, MaterialService service)
        {
            _repo = repo;
            _service = service;
        }

        public Task<List<Material>> ListarAsync() => _repo.GetAllAsync();
        public Task<Material?> ObtenerPorIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<string> CrearAsync(Material material)
        {
            var error = await _service.ValidarNuevoMaterialAsync(material);
            if (error != null) return error;

            // Se fuerza cantidad actual al valor ingresado en registrada
            material.CantidadActual = material.CantidadRegistrada;

            await _repo.AddAsync(material);
            return "Material creado exitosamente.";
        }


        public async Task<string> ActualizarAsync(int id, Material m)
        {
            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Material no encontrado.";

            actual.Titulo = m.Titulo;
            actual.TipoMaterialId = m.TipoMaterialId;
            actual.CantidadRegistrada = m.CantidadRegistrada;
            actual.CantidadActual = m.CantidadActual;

            await _repo.UpdateAsync(actual);
            return "Material actualizado.";
        }

        public async Task<string> EliminarAsync(int id)
        {
            var actual = await _repo.GetByIdAsync(id);
            if (actual == null) return "Material no encontrado.";

            await _repo.DeleteAsync(actual);
            return "Material eliminado.";
        }

        public async Task<string> AgregarStockAsync(int id, int cantidad)
        {
            var material = await _repo.GetByIdAsync(id);
            if (material == null) return "Material no encontrado.";
            if (cantidad <= 0) return "La cantidad debe ser mayor a cero.";

            material.CantidadRegistrada += cantidad;
            material.CantidadActual += cantidad;

            await _repo.UpdateAsync(material);
            return "Stock actualizado correctamente.";
        }
    }
}

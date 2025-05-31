using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;

namespace Biblioteca.Domain.Services
{
    public class MaterialService
    {
        private readonly IMaterialRepository _repo;
        private readonly ITipoMaterialRepository _tipoRepo;


        public MaterialService(IMaterialRepository repo, ITipoMaterialRepository tipoRepo)
        {
            _repo = repo;
            _tipoRepo = tipoRepo;
        }

        public async Task<string?> ValidarNuevoMaterialAsync(Material material)
        {
            // Validar existencia del tipo de material
            var existeTipo = await _tipoRepo.GetByIdAsync(material.TipoMaterialId);
            if (existeTipo == null)
                return "El tipo de material no existe.";

            // Validar que no exista otro material con el mismo título
            var existente = await _repo.GetByTituloAsync(material.Titulo);
            if (existente != null)
                return "Ya existe un material con ese título.";

            // Validaciones numéricas
            if (material.CantidadRegistrada < 0)
                return "La cantidad registrada no puede ser negativa.";

            if (material.CantidadActual < 0)
                return "La cantidad actual no puede ser negativa.";

            if (material.CantidadActual > material.CantidadRegistrada)
                return "La cantidad actual no puede exceder la registrada.";

            return null; // Todo está bien
        }


        public async Task<string?> ValidarStockDisponibleAsync(int materialId)
        {
            var material = await _repo.GetByIdAsync(materialId);
            if (material == null) return "Material no encontrado.";
            if (material.CantidadActual <= 0) return "No hay stock disponible.";

            return null;
        }
    }
}

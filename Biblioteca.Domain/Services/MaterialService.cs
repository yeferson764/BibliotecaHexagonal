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
            var existeTipo = await _tipoRepo.GetByIdAsync(material.TipoMaterialId);
            if (existeTipo == null)
                return "El tipo de material no existe.";

            var existente = await _repo.GetByTituloAsync(material.Titulo);
            if (existente != null)
                return "Ya existe un material con ese título.";

            if (material.CantidadRegistrada < 0)
                return "La cantidad registrada no puede ser negativa.";

            if (material.CantidadActual < 0)
                return "La cantidad actual no puede ser negativa.";

            if (material.CantidadActual > material.CantidadRegistrada)
                return "La cantidad actual no puede exceder la registrada.";

            return null;
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

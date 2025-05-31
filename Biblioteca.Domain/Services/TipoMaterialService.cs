using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;

namespace Biblioteca.Domain.Services
{
    public class TipoMaterialService
    {
        private readonly ITipoMaterialRepository _repo;
        private readonly IMaterialRepository _materialRepo;

        public TipoMaterialService(ITipoMaterialRepository repo, IMaterialRepository materialRepo)
        {
            _repo = repo;
            _materialRepo = materialRepo;
        }

        public async Task<string?> ValidarNuevoAsync(TipoMaterial tipo)
        {
            var existe = await _repo.GetByTipoAsync(tipo.Tipo);
            return existe != null ? "Ese tipo de material ya existe." : null;
        }

        public async Task<string?> ValidarEliminacionAsync(int tipoId)
        {
            bool usado = await _materialRepo.ExistsByTipoMaterialIdAsync(tipoId);
            return usado ? "No puedes eliminar: hay materiales asociados." : null;
        }
    }
}

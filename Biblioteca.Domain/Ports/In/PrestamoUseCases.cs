using Biblioteca.Domain.Models;
using Biblioteca.Domain.Ports.Out;
using Biblioteca.Domain.Services;

namespace Biblioteca.Domain.Ports.In
{
    public class PrestamoUseCases
    {
        private readonly IPrestamoRepository _prestamoRepo;
        private readonly IMaterialRepository _materialRepo;
        private readonly PrestamoService _service;

        public PrestamoUseCases(
            IPrestamoRepository prestamoRepo,
            IMaterialRepository materialRepo,
            PrestamoService service)
        {
            _prestamoRepo = prestamoRepo;
            _materialRepo = materialRepo;
            _service = service;
        }

        public async Task<string> PrestarAsync(Prestamo p)
        {
            var error = await _service.ValidarPrestamoAsync(p.PersonaId, p.MaterialId);
            if (error != null) return error;

            var material = await _materialRepo.GetByIdAsync(p.MaterialId);
            if (material == null) return "Material no encontrado.";

            if (material.CantidadActual <= 0)
                return "No hay stock disponible.";

            material.CantidadActual -= 1;
            await _materialRepo.UpdateAsync(material);

            p.FechaPrestamo = DateTime.Now;
            p.FechaDevolucion = null;

            await _prestamoRepo.AddAsync(p);

            return "Préstamo registrado correctamente.";
        }


        public async Task<string> DevolverAsync(int prestamoId)
        {
            var prestamo = await _prestamoRepo.GetByIdAsync(prestamoId);
            if (prestamo == null) return "Préstamo no encontrado.";
            if (prestamo.FechaDevolucion != null) return "Este préstamo ya fue devuelto.";

            prestamo.FechaDevolucion = DateTime.Now;
            await _prestamoRepo.UpdateAsync(prestamo);

            var material = await _materialRepo.GetByIdAsync(prestamo.MaterialId);
            if (material != null)
            {
                material.CantidadActual += 1;
                await _materialRepo.UpdateAsync(material);
            }

            return "Material devuelto correctamente.";
        }

        public Task<List<Prestamo>> ObtenerTodosAsync() => _prestamoRepo.GetAllAsync();
        public Task<Prestamo?> ObtenerPorIdAsync(int id) => _prestamoRepo.GetByIdAsync(id);
        public Task<List<Prestamo>> ObtenerActivosAsync() => _prestamoRepo.GetActivosAsync();
        public Task<List<Prestamo>> ObtenerDevueltosAsync() => _prestamoRepo.GetDevueltosAsync();
        public Task<List<Prestamo>> ObtenerPorPersonaAsync(int personaId) => _prestamoRepo.GetActivosByPersonaAsync(personaId);
    }
}

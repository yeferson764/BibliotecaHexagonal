using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public int MaterialId { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }

        [NotMapped]
        public bool Devuelto => FechaDevolucion.HasValue;
    }
}
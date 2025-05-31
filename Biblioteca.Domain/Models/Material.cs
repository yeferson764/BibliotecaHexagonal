using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int TipoMaterialId { get; set; } // Relación por ID
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int CantidadRegistrada { get; set; }
        public int CantidadActual { get; set; }
    }
}

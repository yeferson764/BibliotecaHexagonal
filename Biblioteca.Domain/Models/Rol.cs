using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string RolName { get; set; }
        public int CapacidadPrestamo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeCreemos.Models;

namespace TeCreemos.Models
{
    public class CatClientes
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Primer Apellido requerido")]
        [MaxLength(50)]
        public string FstApellido { get; set; }

        [Required(ErrorMessage = "Segundo Apellido requerido")]
        [MaxLength(50)]
        public string ScndApellido { get; set; }

        [Required(ErrorMessage = "Fecha de Nacimiento requerida")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Telefono requerido")]
        [MaxLength(13)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Clave de Elector requerido")]
        [MaxLength(18)]
        public string ClaveElector { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeCreemos.Models
{
    public class LogTransacciones
    {
        [Required(ErrorMessage = "IdCuenta requerido")]
        public int IdCuenta { get; set; }

        [Required(ErrorMessage = "Operacion requerido")]
        public string Operacion { get; set; }

        [Required(ErrorMessage = "Cantidad requerido")]
        [MaxLength(10)]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "Saldo Anterior requerido")]
        public decimal SaldoAnterior { get; set; }

        [Required(ErrorMessage = "Saldo Restante requerido")]
        public decimal SaldoRestante { get; set; }

        [Required(ErrorMessage = "Fecha requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Hora requerida")]
        public TimeSpan Hora { get; set; }
    }
}

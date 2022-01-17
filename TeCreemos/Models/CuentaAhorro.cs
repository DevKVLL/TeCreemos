using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeCreemos.Models;

namespace TeCreemos.Models
{
    public class CuentaAhorro
    {
        [Required(ErrorMessage = "IdCliente requerido")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Numero de Cuenta requerido")]
        [MaxLength(16)]
        [MinLength(16)]
        public string NoCuenta { get; set; }

        [Required(ErrorMessage = "Saldo Actual requerido")]
        public decimal SaldoActual { get; set; }
    }

    public class DetailsCuentas
    {
        public string FechaApertura { get; set; }
        public string NoCuenta { get; set; }
        public string Saldo { get; set; }
        public string Detalle { get; set; }
    }

    public class Account
    {
        public int IdCuenta { get; set; }

        public string Nombre { get; set; }

        public string FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        public string ClaveElector { get; set; }

        public string FechaApertura { get; set; }

        public string NoCuenta { get; set; }

        public string Saldo { get; set; }
    }

    public class Log
    {
        public int IdCuenta { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string SaldoAnterior { get; set; }

        public string Operacion { get; set; }

        public string Cantidad { get; set; }

        public string Restante { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using TeCreemos.Models;

namespace TeCreemos.Controllers
{
    public class CuentasAhorroController : Controller
    {
        private readonly IConfiguration configuration;
        public DB db = new DB();

        public CuentasAhorroController(IConfiguration config)
        {
            configuration = config;
        }

        public ActionResult CarteraClientes()
        {
            return View();
        }

        public ActionResult Cuentas(int? id)
        {
            CatClientes model = getAccounts(id);
            return View(model);
        }

        public ActionResult Account(int? id)
        {
            Account model = getCuentaAhorro(id);

            return View(model);
        }

        // GET: CuentasAhorro
        [Produces("application/json")]
        [HttpGet]
        public ActionResult getCuentas(int? id)
        {
            var query = "SELECT CONVERT(varchar, FechaApertura, 103) as FechaApertura, NoCuenta, convert(varchar(50),SaldoActual) AS Saldo, ('<a href=" + "''" + "/CuentasAhorro/Account/' + cast(IdCuenta AS varchar(50)) + ''' style=" + "''" + "height:24px;" + "''" + " src=" + "''" + "~/images/detalle.png" + "''" + ">Detalle</a>')AS Detalle FROM CuentaAhorro WHERE IdCliente=" + id + " ORDER BY FechaApertura ASC";

            SqlConnection con = db.OpenConnection(configuration);

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader dr = cmd.ExecuteReader();

            List<DetailsCuentas> lst = new List<DetailsCuentas>();
            while (dr.Read())
            {
                lst.Add(new DetailsCuentas()
                {
                    FechaApertura = dr["FechaApertura"].ToString(),
                    NoCuenta = dr["NoCuenta"].ToString(),
                    Saldo = "$" + dr["Saldo"].ToString(),
                    Detalle = dr["Detalle"].ToString()
                });
            }

            con.Close();

            var jsonResult = Json(lst.ToList());
            return jsonResult;
        }

        [Produces("application/json")]
        [HttpGet]
        public ActionResult getClientes()
        {
            var query = "SELECT (Nombre+' '+FstApellido+' '+ScndApellido) AS Nombre, Telefono, Correo, ClaveElector, ('<a href=" + "''" + "CuentasAhorro/Cuentas/'+cast(IdCliente as varchar(50))+''' style=" + "''" + "height:24px;" + "''" + " src=" + "''" + "images/detalle.png" + "''" + ">Detalle</a>')AS Detalle FROM CatClientes ORDER BY Nombre ASC";

            SqlConnection con = db.OpenConnection(configuration);

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader dr = cmd.ExecuteReader();

            List<DetailsCliente> lst = new List<DetailsCliente>();
            while (dr.Read())
            {
                lst.Add(new DetailsCliente()
                {
                    Nombre = dr["Nombre"].ToString(),
                    ClaveElector = dr["ClaveElector"].ToString(),
                    Telefono = dr["Telefono"].ToString(),
                    Correo = dr["Correo"].ToString(),
                    Detalle = dr["Detalle"].ToString()
                });
            }

            con.Close();

            var jsonResult = Json(lst.ToList());
            return jsonResult;
        }

        [Produces("application/json")]
        [HttpGet]
        public ActionResult getOperaciones(int? id)
        {
            var query = "SELECT CONVERT(varchar, Fecha, 103) AS Fecha, convert(varchar(50),Hora) AS Hora, convert(varchar(50),SaldoAnterior) AS SaldoAnterior, Operacion, convert(varchar(50),Cantidad) AS Cantidad, convert(varchar(50),SaldoRestante) AS Restante FROM LogTransacciones WHERE IdCuenta="+id+" ORDER BY Fecha ASC";

            SqlConnection con = db.OpenConnection(configuration);

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader dr = cmd.ExecuteReader();

            List<Log> lst = new List<Log>();
            while (dr.Read())
            {
                lst.Add(new Log()
                {
                    Fecha = dr["Fecha"].ToString(),
                    Hora = dr["Hora"].ToString(),
                    SaldoAnterior = dr["SaldoAnterior"].ToString(),
                    Operacion = dr["Operacion"].ToString(),
                    Cantidad = dr["Cantidad"].ToString(),
                    Restante = dr["Restante"].ToString()
                });
            }

            con.Close();

            var jsonResult = Json(lst.ToList());
            return jsonResult;
        }

        public string SaveCliente(CatClientes Cliente)
        {
            try
            {
                SqlConnection con = db.OpenConnection(configuration);
                Cliente.FechaNacimiento = DateTime.Parse(Cliente.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss"));
                string query = "INSERT INTO CatClientes VALUES('" + Cliente.Nombre + "', '" + Cliente.FstApellido + "', '" + Cliente.ScndApellido + "', '" + Cliente.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Cliente.Telefono + "', '" + Cliente.Correo + "', '" + Cliente.ClaveElector + "')";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
                return "success";
            }
            catch
            {
                return "error";
            }
        }

        public string AperturarCuenta(CuentaAhorro ca)
        {
            try
            {
                int count = verifNumCuenta(ca.NoCuenta);

                if (count != 0)
                {
                    return "existente";
                }
                else
                {
                    SqlConnection con = db.OpenConnection(configuration);

                    string query = "INSERT INTO CuentaAhorro (IdCliente,FechaApertura, NoCuenta, SaldoActual) VALUES(" + ca.IdCliente + ", GETDATE(), '" + ca.NoCuenta + "', " + ca.SaldoActual + ")";
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataReader dr = cmd.ExecuteReader();
                    con.Close();
                    return "success";
                }
            }
            catch
            {
                return "error";
            }
        }

        public string Operacion(int IdCuenta, string Operacion, decimal Cantidad)
        {
            try
            {
                Account model = getCuentaAhorro(IdCuenta);
                decimal rest;
                if (Operacion=="Deposito")
                {
                    rest = decimal.Parse(model.Saldo) + Cantidad;
                }
                else
                {
                    rest = decimal.Parse(model.Saldo) - Cantidad;
                }

                string query1 = "UPDATE CuentaAhorro SET SaldoActual = "+rest+" WHERE IdCuenta = "+IdCuenta;
                SqlConnection con1 = db.OpenConnection(configuration);
                SqlCommand cmd1 = new SqlCommand(query1, con1);

                SqlDataReader dr1 = cmd1.ExecuteReader();
                con1.Close();

                SqlConnection con = db.OpenConnection(configuration);
                string query = "INSERT INTO LogTransacciones (IdCuenta, Operacion, Cantidad, SaldoAnterior, SaldoRestante, Fecha, Hora) VALUES("+IdCuenta+", '"+Operacion+"', "+Cantidad+", "+model.Saldo+", "+rest+ ", GETDATE(), CONVERT(VARCHAR(5),getdate(),108))";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
                return Convert.ToString(rest);
            }
            catch
            {
                return "error";
            }
        }

        public CatClientes getAccounts(int? id)
        {
            SqlConnection con = db.OpenConnection(configuration);
            string query = "SELECT * FROM CatClientes WHERE IdCliente=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            CatClientes model = new CatClientes();
            while (dr.Read())
            {
                model = new CatClientes
                {
                    IdCliente = int.Parse(dr["IdCliente"].ToString()),
                    Nombre = dr["Nombre"].ToString(),
                    FstApellido = dr["FstApellido"].ToString(),
                    ScndApellido = dr["ScndApellido"].ToString(),
                    FechaNacimiento = DateTime.Parse(dr["FechaNacimiento"].ToString()),
                    Telefono = dr["Telefono"].ToString(),
                    Correo = dr["Correo"].ToString(),
                    ClaveElector = dr["ClaveElector"].ToString()
                };
            }
            con.Close();
            return model;
        }

        public Account getCuentaAhorro(int? id)
        {
            SqlConnection con = db.OpenConnection(configuration);
            string query = "SELECT ca.IdCuenta, CONVERT(varchar, ca.FechaApertura, 103) as FechaApertura, ca.NoCuenta, convert(varchar(50),ca.SaldoActual) AS Saldo, (cc.Nombre + ' ' + cc.FstApellido + ' ' + cc.ScndApellido) AS Nombre, CONVERT(varchar, cc.FechaNacimiento, 103) AS FechaNacimiento, cc.Telefono, cc.Correo, cc.ClaveElector FROM CuentaAhorro ca INNER JOIN CatClientes cc ON cc.IdCliente = ca.IdCliente WHERE ca.IdCuenta = "+id+" ORDER BY ca.FechaApertura ASC";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            Account model = new Account();
            while (dr.Read())
            {
                model = new Account()
                {
                    IdCuenta = int.Parse(dr["IdCuenta"].ToString()),
                    Nombre = dr["Nombre"].ToString(),
                    FechaNacimiento = dr["FechaNacimiento"].ToString(),
                    Telefono = dr["Telefono"].ToString(),
                    Correo = dr["Correo"].ToString(),
                    ClaveElector = dr["ClaveElector"].ToString(),
                    FechaApertura = dr["FechaApertura"].ToString(),
                    NoCuenta = dr["NoCuenta"].ToString(),
                    Saldo = dr["Saldo"].ToString()
                };
            }
            con.Close();
            return model;
        }

        public int verifNumCuenta(string NoCuenta)
        {
            SqlConnection con = db.OpenConnection(configuration);
            var query = "SELECT COUNT(IdCuenta) FROM CuentaAhorro WHERE NoCuenta='"+NoCuenta+"'";

            SqlCommand cmd = new SqlCommand(query, con);
            Int32 count = (Int32)cmd.ExecuteScalar();
            con.Close();
            return count;
        }
    }
}
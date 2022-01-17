using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TeCreemos.Models;

namespace TeCreemos.Models
{
    public class DB
    {
        public string ConnectionStr(IConfiguration _configuration)
        {
            string con = _configuration.GetConnectionString("dbTC");
            return con;
        }

        public SqlConnection OpenConnection(IConfiguration _configuration)
        {
            SqlConnection objCon = new SqlConnection(ConnectionStr(_configuration));
            objCon.Open();
            return objCon;
        }
    }
}

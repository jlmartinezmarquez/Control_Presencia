using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Infra.DataAccess.Repositories
{
    public abstract class Repository
    {
        private readonly string connectionString;
        public Repository()
        {
            //connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conMDB"].ToString();
            connectionString = @"Server=RYZEN-7\SQLEXPRESS;Database=MDB;Integrated Security=SSPI;";

        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}

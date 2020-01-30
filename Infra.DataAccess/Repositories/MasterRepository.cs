using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Infra.DataAccess.Repositories
{
    public abstract class MasterRepository:Repository
    {
        protected List<SqlParameter> parameters;

        protected int ExecuteNonQuery(string transactSql) 
        {
            int iRet = -1;
            using (var connection= GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    foreach (SqlParameter item in parameters)
                    {
                        command.Parameters.Add(item);
                    }
                    iRet = command.ExecuteNonQuery();
                    parameters.Clear();
                }
            }
            return iRet;
        }
        protected DataTable ExecuteReader(string transactSql) 
        {
            var table = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }
    }
}

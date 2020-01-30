using System;
using System.Collections.Generic;
using System.Text;
using Infra.DataAccess.Contracts;
using Infra.DataAccess.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Infra.DataAccess.Repositories
{
    public class EmpleadoRepository : MasterRepository, IEmpleadoRepository
    {
        private string selectAll;
        private string insert;
        private string update;
        private string delete;
        
        public EmpleadoRepository()
        {
            selectAll = "select * from Empleado";
            insert = "insert into Empleado values(@Id, @Nombre, @Apellido, @Codigo)";
            update = "update Empleado set Id=@Id, Nombre=@Nombre , Apellido=@Apellido, Codigo=@Codigo";
            delete = "delete from Empleado where Id=@Id";
        }

        public int Add(Empleado entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Nombre", entity.Nombre));
            parameters.Add(new SqlParameter("@Apellido", entity.Apellido));
            parameters.Add(new SqlParameter("@Codigo", entity.Codigo));
            return ExecuteNonQuery(insert);
        }
        public int Delete(int Id)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            return ExecuteNonQuery(delete);
        }
        public int Edit(Empleado entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", entity.ID));
            parameters.Add(new SqlParameter("@Nombre", entity.Nombre));
            parameters.Add(new SqlParameter("@Apellido", entity.Apellido));
            parameters.Add(new SqlParameter("@Codigo", entity.Codigo));
            return ExecuteNonQuery(update);
        }
        public IEnumerable<Empleado> GetAll()
        {
            var tableResult = ExecuteReader(selectAll);
            var listEmpleados = new List<Empleado>();
            foreach (DataRow item in tableResult.Rows)
            {
                listEmpleados.Add(new Empleado
                {
                    ID = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Apellido = item[2].ToString(),
                    Codigo = item[3].ToString()
                });
            }
            return listEmpleados;
        }
    }
}

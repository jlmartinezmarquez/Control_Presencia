using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DataAccess.Entities
{
    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Codigo { get; set; }
    }
}

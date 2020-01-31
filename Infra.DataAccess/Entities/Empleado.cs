using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DataAccess.Entities
{
    public class Empleado
    {
        // JLM - Convencion sobre nombres: las propiedades publicas siempre se escriben en Pascal Case. Asi, seria Id en vez de ID
        // JLM - Es muy probable que tener instalado Style Cop te ayude con estas cosas
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Codigo { get; set; }
    }
}

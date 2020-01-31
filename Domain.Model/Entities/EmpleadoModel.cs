using System;
using System.Collections.Generic;
using System.Text;
using Infra.DataAccess.Contracts;
using Infra.DataAccess.Repositories;
using Infra.DataAccess.Entities;
using Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities
{
    public class EmpleadoModel
    {
        private IEmpleadoRepository empleadoRepository;
        public EntityState State { private get; set; }

        public int ID { get; set; }
        [Required(ErrorMessage ="El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo Codigo es obligatorio.")]
        [RegularExpression("([0-9]+)",ErrorMessage ="El campo Codigo solo puede contener números.")]
        [StringLength(maximumLength:13,MinimumLength =13,ErrorMessage = "El campo Codigo debe tener 13 caracteres.")]
        public string Codigo { get; set; }
        public string NombreCompleto { get; private set; }
        public EmpleadoModel()
        {
            empleadoRepository = new EmpleadoRepository();
        }
        
        // JLM - No metas codigo de negocio en una entity, ya que rompe el principio de Single Responsibility. Una clase para las propiedades, otra para los métodos con lógica
        public string SaveChanges()
        {
            string message=null;
            try
            {
                var empleadoDataModel = new Empleado();
                empleadoDataModel.ID = ID;
                empleadoDataModel.Nombre = Nombre;
                empleadoDataModel.Apellido = Apellido;
                empleadoDataModel.Codigo = Codigo;

                // JLM - ESta instruccion solo se deberia usar en los casos puntuales en los que reemplazarla por otra cosa mejor fuera muy costoso.
                // JLM - Si mañana necesitas añadir EntityState.Updated habria que modificar la clase, lo que rompe el principio Open/Closed
                // JLM - La manera de no usar switch case y respetar la O de SOLID es crearte por cada case una clase con un metodo que implemente la logica que tienes en el case
                // JLM - Basicamente, implementar el Strategy Pattern: ﻿https://dotnetcodr.com/2015/09/30/design-patterns-and-practices-in-net-the-strategy-pattern/
                // JLM - Ej: IEntityStateCrud con un método llamado Operate. Es implementado por EntityStateAdd, EntityStateModify, EntityStateDelete
                // JLM - En vez de usar Reflection, que ralentiza el codigo, le puedes inyectar al constructor un Dictionary con las clases y el interfaz que implementa
                switch (State)
                {
                    case EntityState.Added:
                        {
                            // Ejecutar reglas comerciales / calculos
                            empleadoRepository.Add(empleadoDataModel);
                            message = "Añadido satisfactoriamente.";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            empleadoRepository.Edit(empleadoDataModel);
                            message = "Actualizado satisfactoriamente.";
                            break;
                        }
                    case EntityState.Deleted:
                        {
                            empleadoRepository.Delete(ID);
                            message = "Eliminado satisfactoriamente.";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    message = "Registro duplicado.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<EmpleadoModel> GetAll()
        {
            var empleadoDataModel = empleadoRepository.GetAll();
            var listEmpleados = new List<EmpleadoModel>();
            foreach (Empleado item in empleadoDataModel)
            {
                listEmpleados.Add(new EmpleadoModel
                {
                    ID = item.ID,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Codigo = item.Codigo,
                    NombreCompleto = item.Nombre + " " + item.Apellido
                }); ;
            }
            return listEmpleados;
        }
    }
}

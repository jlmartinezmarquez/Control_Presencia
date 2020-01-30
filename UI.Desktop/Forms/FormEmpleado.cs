using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain.Model;
using Domain.Model.Entities;
using Domain.Model.ValueObjects;

namespace UI.Desktop.Forms
{
    public partial class FormEmpleado : Form
    {
        private EmpleadoModel empleadoModel = new EmpleadoModel();
        public FormEmpleado()
        {
            InitializeComponent();
        }
        private void FormEmpleado_Load(object sender, System.EventArgs e)
        {
            ListEmpleados();
        }
        private void ListEmpleados()
        {
            try
            {
                dataGridView1.DataSource = empleadoModel.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using ClaseMaestra;

namespace Junior_Parcial1_Ap2
{
    public partial class Materiales : System.Web.UI.Page
    {
      
        DataTable dt = new DataTable();
        Material material = new Material();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Material"), new DataColumn("Cantidad") });
            ViewState["Material"] = dt;

        }
        public void CargarDatos()
        {
            DbConexion cone = new DbConexion();
            int id = 0;
            int.TryParse(TextBoxId.Text, out id);
            material.Buscar(id);

            TextBoxId.Text = material.MaterialId.ToString();
            TextBoxRazon.Text = material.Razon;
            ViewState["Material"] = dt;
            //mater
            
            //foreach( )
            //{
            //    material.AgregarMateriales(row.cel)
            //}

        }

        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(TextBoxId.Text, out id);

          
        }

        protected void ButtonNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        public void Limpiar()
        {
            TextBoxRazon.Text = "";
            TextBoxId.Text = "";
            TextBoxMaterial.Text = "";
            TextBoxCantidad.Text = "";
           // GridViewMaterial.DataSource()
        }

        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
            
        }

        protected void ButtonEliminar_Click(object sender, EventArgs e)
        {
            int id = 0;
           
            int.TryParse(TextBoxId.Text, out id);
            material.MaterialId = id;

            if (material.Eliminar())
            {
               
            }
        }
    }
}
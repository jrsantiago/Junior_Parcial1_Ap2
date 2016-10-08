using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Junior_Parcial1_Ap2
{
    public partial class MaterialesRegistro : System.Web.UI.Page
    {
        Material mate = new Material();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LLamarValores()
        {
            DescripcionTextBox.Text = mate.Descripcion;
            PrecioTextBox.Text = mate.Precio.ToString();

        }
        public int ConvertirId()
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            return id;
        }
        public void AgregarValores()
        {
            float precio = 0;
            float.TryParse(PrecioTextBox.Text, out precio);

            mate.Descripcion = DescripcionTextBox.Text;
            mate.Precio = precio;
        }


        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();

            if (string.IsNullOrEmpty(IdTextBox.Text))
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            } else
            {

                  mate.Buscar(id);
                  LLamarValores();

            }

        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();
            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text) && string.IsNullOrWhiteSpace(PrecioTextBox.Text))
                {
                    Response.Write("<script>alert('LLene todos los Campos')</script>");
                }
                else
                {
                    AgregarValores();
                    mate.Insertar();
                    Response.Write("<script>alert('Se a Guardado')</script>");
                }


            } else if (IdTextBox.Text != "")
            {
                if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text) && string.IsNullOrWhiteSpace(PrecioTextBox.Text))
                {
                    Response.Write("<script>alert('Error')</script>");
                }
                else
                {
                    AgregarValores();
                    mate.IdMaterial = ConvertirId();
                    mate.Editar();
                    Response.Write("<script>alert('Se ha Editado')</script>");
                }

            }

        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MaterialesRegistro.aspx");
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();
            if(string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            }else
            {
                mate.IdMaterial = id;
                mate.Eliminar();
                Response.Write("<script>alert('Se ha Eliminado')</script>");
                Response.Redirect("~/MaterialesRegistro.aspx");

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Junior_Parcial1_Ap2
{
    public partial class SolicitudRegistro : System.Web.UI.Page
    {
        Solicitudes sol = new Solicitudes();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FechaCalendar.Visible = false;
            }
        }
        public void AgregarValores()
        {
            float Total = 0;
            float.TryParse(TotalTextBox.Text, out Total);

            sol.Fecha = FechaTextBox0.Text;
            sol.Razon = RazonTextBox.Text;
            sol.Total = Total;
        }
        public void LLamarValores()
        {
            FechaTextBox0.Text = sol.Fecha;
            TotalTextBox.Text = sol.Total.ToString();
            RazonTextBox.Text = sol.Razon;

        }
        public int ConvertirId()
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            return id;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();

            if (string.IsNullOrEmpty(IdTextBox.Text))
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            }
            else
            {

                sol.Buscar(id);
                LLamarValores();

            }
        }

        protected void FechaButton_Click(object sender, EventArgs e)
        {
            if (FechaCalendar.Visible)
            {
                FechaCalendar.Visible = false;
            }
            else
            {
                FechaCalendar.Visible = true;
                FechaLabel.Visible = true;
            }
        }

        protected void FechaCalendar_SelectionChanged(object sender, EventArgs e)
        {
            FechaTextBox0.Text = FechaCalendar.SelectedDate.ToLongDateString();
            FechaCalendar.Visible = false;
            FechaLabel.Visible = false;
        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SolicitudRegistro.aspx");
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();
            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            }
            else
            {
                sol.IdSolicitud = id;
                sol.Eliminar();
                Response.Write("<script>alert('Se ha Eliminado')</script>");
                Response.Redirect("~/SolicitudRegistro.aspx");
            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            int id = ConvertirId();
            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                if (string.IsNullOrWhiteSpace(FechaTextBox0.Text) || string.IsNullOrWhiteSpace(TotalTextBox.Text) || string.IsNullOrWhiteSpace(RazonTextBox.Text))
                {
                    Response.Write("<script>alert('LLene todos los Campos')</script>");
                }
                else
                {
                    AgregarValores();
                    sol.Insertar();
                    Response.Write("<script>alert('Se ha Guardado')</script>");
                }


            }
            else if (IdTextBox.Text != "")
            {
                if (string.IsNullOrWhiteSpace(FechaTextBox0.Text) && string.IsNullOrWhiteSpace(TotalTextBox.Text) && string.IsNullOrWhiteSpace(RazonTextBox.Text))
                {
                    Response.Write("<script>alert('Error')</script>");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(FechaTextBox0.Text) || string.IsNullOrWhiteSpace(TotalTextBox.Text) || string.IsNullOrWhiteSpace(RazonTextBox.Text))
                    {
                        Response.Write("<script>alert('LLene todos los Campos')</script>");
                    }
                    else
                    {
                         AgregarValores();
                         sol.IdSolicitud = ConvertirId();
                         sol.Editar();
                         Response.Write("<script>alert('Se ha Editado')</script>");
                    }
                   
                }

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace Junior_Parcial1_Ap2
{
    public partial class SoliDetalle : System.Web.UI.MasterPage
    {
        Solicitudes sol = new Solicitudes();
        DataTable dtTa = new DataTable();
        Material mate = new Material();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FechaCalendar.Visible = false;
                AddColumnas();
            }

            dtTa = mate.Listado("*", "0=0", "ORDER BY Descripcion");
            for (int i = 0; i <= dtTa.Rows.Count - 1; i++)
                MaterialDropDownList.Items.Add(Convert.ToString(mate.Listado("*", "0=0", "ORDER BY Descripcion").Rows[i]["Descripcion"]));
        }
        private void ObtenerValores()
        {
            int id = 0;

            if (string.IsNullOrWhiteSpace(RazonTextBox.Text) || string.IsNullOrWhiteSpace(FechaTextBox.Text) || string.IsNullOrWhiteSpace(TotalTextBox.Text) || string.IsNullOrWhiteSpace(PrecioTextBox0.Text))
            {
                Response.Write("<script>alert('Introdusca Los Campos')</script>");
            }
            else
            {


                int.TryParse(IdTextBox.Text, out id);
                sol.IdSolicitud = id;
                sol.Razon = RazonTextBox.Text;
                sol.Fecha = FechaTextBox.Text;
                sol.Total += Convert.ToInt32(PrecioTextBox0.Text);
                foreach (GridViewRow row in MaterialGridView.Rows)
                {
                    sol.InsertarMaterial(id, id, Convert.ToInt32(row.Cells[1].Text), Convert.ToSingle(row.Cells[2].Text));
                }

            }
        }
        public void LlenarValores()
        {
            DataTable dt = new DataTable();
            RazonTextBox.Text = sol.Razon;
            FechaTextBox.Text = sol.Fecha;
            TotalTextBox.Text = sol.Total.ToString();
           foreach(var item in sol.Detalle)
            {
                dt.Rows.Add(item.IdMaterial, item.Cantidad, item.Precio);
                ObtenerValGridView();
            }

        }
        public void ObtenerValGridView()
        {
            MaterialGridView.DataSource = (DataTable)ViewState["Detalle"];
            MaterialGridView.DataBind();
        }
        public void AddColumnas()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Material"), new DataColumn("Cantidad"),new DataColumn("Precio")});
            ViewState["Detalle"] = dt;
        }
        protected void IdButton_Click(object sender, EventArgs e)
        {
           
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            if (IdTextBox.Text == "")
            {
                Response.Write("<script>alert('Introdusca el Id Campos')</script>");
            }
             else
            {
                sol.Buscar(id);
               LlenarValores();
               
                    
            }
        }
        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            int pre = Convert.ToInt32(TotalTextBox.Text);
            
            try
            {
                 if (MaterialDropDownList.Text != "")
                {
                DataTable dt = (DataTable)ViewState["Detalle"];
                DataRow row;

                    

                    row = dt.NewRow();
                    row["Material"] = MaterialDropDownList.SelectedValue;

                    row["Cantidad"] = CantidadTextBox.Text;                
                    row["Precio"] =PrecioTextBox0.Text;
                    dt.Rows.Add(row);
                    ViewState["Detalle"] = dt;
                    
       
                    ObtenerValGridView();
                    TotalTextBox.Text = pre.ToString();
                 }
                 else
                 {
                        Response.Write("<script>alert('Debe seleccionar un Material')</script>");
                  }
                CantidadTextBox.Text = "";
                PrecioTextBox0.Text = "";
               
            }
            catch(Exception)
            {

            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
           
            if (IdTextBox.Text == "")
            {
                if (string.IsNullOrWhiteSpace(RazonTextBox.Text) || string.IsNullOrWhiteSpace(PrecioTextBox0.Text) || string.IsNullOrWhiteSpace(FechaTextBox.Text))
                {
                    Response.Write("<script>alert('Llene Todos Los Campos')</script>");
                }
                else
                { ObtenerValores();
                    if (sol.Insertar())
                    {
                        Response.Write("<script>alert('Se ha Guardado')</script>");
                    }
                }
            }
            else if (string.IsNullOrWhiteSpace(RazonTextBox.Text) || string.IsNullOrWhiteSpace(PrecioTextBox0.Text) || string.IsNullOrWhiteSpace(FechaTextBox.Text))
            {
                Response.Write("<script>alert('Llene Todos Los Campos')</script>");
            }
            else
            {
                ObtenerValores();
                if (sol.Editar())
                {
                    Response.Write("<script>alert('Se ha Modificado')</script>");
                }
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
            FechaTextBox.Text = FechaCalendar.SelectedDate.ToLongDateString();
            FechaCalendar.Visible = false;
            FechaLabel.Visible = false;
        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SDetalle.aspx");

        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            if (IdTextBox.Text.Length == 0)
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            }
            else
            {
                sol.IdSolicitud = id;
                
                    if (sol.Eliminar())
                    {
                        Response.Write("<script>alert('Eliminado correctamente')</script>");
                        Response.Redirect("~/SDetalle.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al eliminar')</script>");
                    }
                }
             
            }
        }
    }

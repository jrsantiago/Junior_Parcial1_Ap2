using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Solicitudes : ClaseMaestra
    {
        public int IdSolicitud { get; set; }
        public string Fecha { get; set; }
        public string Razon { get; set; }
        public float Total { get; set; }
        public List<SolicitudesDetalle> Detalle { get; set; }

        public Solicitudes()
        {
            this.IdSolicitud = 0;
            this.Fecha = "";
            this.Razon = "";
            this.Total = 0;
            Detalle = new List<SolicitudesDetalle>();
        }
       
        public override bool Buscar(int IdBuscado)
        {
            DbConexion conexion = new DbConexion();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            bool retornar = true;
            try
            {
                dt = conexion.ObtenerDatos("SELECT * FROM Solicitudes WHERE IdSolicitud=" + IdBuscado);
                if (dt.Rows.Count > 0)
                {
                    this.IdSolicitud = (int)dt.Rows[0]["IdSolicitud"];
                    this.Fecha = dt.Rows[0]["Fecha"].ToString();
                    this.Razon = dt.Rows[0]["Razon"].ToString();
                    this.Total =Convert.ToSingle(dt.Rows[0]["Total"]);

                    dt2 = conexion.ObtenerDatos("SELECT * FROM SolicitudesDetalle WHERE IdSolicitud=" + IdBuscado);
                    foreach (DataRow item in dt2.Rows)
                    {
                        this.InsertarMaterial(IdBuscado,(int)item["IdMaterial"], (int)item["Cantidad"], (float)item["Precio"]);
                    }
                }
                return dt.Rows.Count > 0;
            }
            catch (Exception)
            {
                retornar = false;
            }
            return retornar;
        }
        public override bool Insertar()
        {
            DbConexion cone = new DbConexion();
            int retorno = 0;
            object Identity;

            try
            {
                Identity = cone.ObtenerValor(string.Format("INSERT INTO Solicitudes (Fecha, Razon,Total) VALUES ('{0}','{1}',{2}) SELECT @@Identity", this.Fecha, this.Razon, this.Total));
                int.TryParse(Identity.ToString(), out retorno);
                if (retorno> 0)
                {
                    foreach (SolicitudesDetalle item in this.Detalle)
                    {
                        cone.Ejecutar(string.Format("Insert Into SolicitudesDetalle(IdSolicitud,Cantidad,Precio) VALUES ({0},{1},{2})", retorno, retorno, item.Cantidad, item.Precio));
                    }
                }
                return retorno > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override bool Editar()
        {
            DbConexion conexion = new DbConexion();
            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(string.Format("UPDATE Solicitudes SET Fecha='{0}', Total={1} ,Razon ='{2}' WHERE IdSolicitud={3}", this.Fecha,this.Total, this.Razon, this.IdSolicitud));
                if (retorno)
                {
                    conexion.Ejecutar(String.Format("DELETE FROM SolicitudesDetalle WHERE IdSolicitud={0}", this.IdSolicitud));
                    foreach (SolicitudesDetalle item in this.Detalle)
                    {
                        conexion.Ejecutar(string.Format("Insert Into SolicitudesDetalle (IdSolicitud,Cantidad,Precio) VALUES ({0},{1},{2})", IdSolicitud, item.Cantidad, item.Precio));
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return retorno;
        }
        public override bool Eliminar()
        {
            DbConexion conexion = new DbConexion();
            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(String.Format("DELETE FROM SolicitudesDetalle WHERE IdSolicitud={0}", this.IdSolicitud));
                if (retorno)
                {
                    conexion.Ejecutar(String.Format("DELETE FROM Solicitudes WHERE IdSolicitud={0}", this.IdSolicitud));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
       
        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            DbConexion conexion = new DbConexion();
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = " orden by  " + Orden;
            return conexion.ObtenerDatos(("SELECT " + Campos + " FROM Solicitudes WHERE " + Condicion + ordenar));
        }
        public void InsertarMaterial(int solocitud,int material ,int Cantidad,float Precio)
        {
            this.Detalle.Add(new SolicitudesDetalle(solocitud, material,Cantidad,Precio));
        }
    }
}

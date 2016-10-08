using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Material : ClaseMaestra
    {
        public int IdMaterial { get; set; }
        public string  Descripcion { get; set; }
        public float  Precio { get; set; }
        public List<Material> Detalle { get; set; }   
             
        public Material()
        {
            this.IdMaterial = 0;
            this.Descripcion = "";
            this.Precio = 0f;
            Detalle = new List<Material>();
        }
        public override bool Buscar(int IdBuscado)
        {
            DbConexion conexion = new DbConexion();
            DataTable dt = new DataTable();
            bool retornar = true;
            try
            {

                dt = conexion.ObtenerDatos("SELECT * FROM Materiales WHERE IdMaterial=" + IdBuscado);
                if (dt.Rows.Count > 0)
                {
                    this.IdMaterial = (int)dt.Rows[0]["IdMaterial"];
                    this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    this.Precio = Convert.ToSingle(dt.Rows[0]["Precio"]);

                }
            }catch(Exception)
            {
               retornar = false;
            }
            return retornar;
        }

        public override bool Editar()
        {
            DbConexion conexion = new DbConexion();
            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(String.Format("UPDATE Materiales SET Descripcion='{0}', Precio={1} WHERE IdMaterial={2}", this.Descripcion, this.Precio, this.IdMaterial));
              
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
                retorno = conexion.Ejecutar(String.Format("DELETE FROM Materiales WHERE IdMaterial={0}", this.IdMaterial));
                //if (retorno)
                //    conexion.Ejecutar(String.Format("DELETE FROM Materiales WHERE IdMaterial={0}", this.IdMaterial));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public override bool Insertar()
        {
            DbConexion conexion = new DbConexion();
            int retorno = 0;
           
            try
            {
                
              conexion.ObtenerValor(String.Format("INSERT INTO Materiales (Descripcion, Precio) VALUES ('{0}',{1})", this.Descripcion, this.Precio));
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno > 0;
        }
        public string PrecioDetalle(string material)
        {
            string retorno = "";
            DbConexion conexion = new DbConexion();
            DataTable dt = new DataTable();
            bool retornar = true;
            try
            {

                dt = conexion.ObtenerDatos("SELECT * FROM Materiales WHERE Descripcion =" + material);
                if (dt.Rows.Count > 0)
                {
                    this.IdMaterial = (int)dt.Rows[0]["IdMaterial"];
                    this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    this.Precio = Convert.ToSingle(dt.Rows[0]["Precio"]);

                }
            }
            catch (Exception)
            {
                retornar = false;
            }
            return retorno;

        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            DbConexion conexion = new DbConexion();
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = " orden by  " + Orden;
            return conexion.ObtenerDatos(("SELECT " + Campos + " FROM Materiales WHERE " + Condicion + Orden));
        }
    }
}

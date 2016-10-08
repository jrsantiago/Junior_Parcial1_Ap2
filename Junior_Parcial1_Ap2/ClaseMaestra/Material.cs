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
        public int MaterialId { get; set; }
        public string  Razon { get; set; }
        public List<MaterialesDetalle> Detalle { get; set; }

        public Material()
        {
            this.MaterialId = 0;
            this.Razon = "";
            Detalle = new List<MaterialesDetalle>();
        }
        public override bool Buscar(int IdBuscar)
        {
            DataTable dt = new DataTable();
            DataTable dtDetalle = new DataTable();
            DbConexion cone = new DbConexion();

            try
            {
                dt = cone.ObtenerDatos(String.Format("Select * from Material where MaterialId=)"+ IdBuscar));
                this.MaterialId = (int)dt.Rows[0]["MaterialId"];
                this.Razon = dt.Rows[0]["Razon"].ToString();

                dtDetalle = cone.ObtenerDatos(String.Format("Select * from MaterialDetalle where MaterialId+", IdBuscar));
                if(dt.Rows.Count>0)
                {
                    foreach(DataRow row in dtDetalle.Rows)
                    {
                        AgregarMateriales(row[0].ToString(), (int)row[0]);
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return dt.Rows.Count > 0;
        }

        public override bool Editar()
        {
            throw new NotImplementedException();
        }

        public override bool Insertar()
        {
            DbConexion cone = new DbConexion();
            int Retornar = 0;
            DataTable dt = new DataTable();
            try
            {
                Retornar =Convert.ToInt32(cone.Ejecutar(String.Format("Insert Into Material(Razon) Values('{0}') Select @@IDENTITY",this.Razon)));
                if(Retornar>0)
                {
                    foreach(MaterialesDetalle item in Detalle)
                    {
                        cone.Ejecutar(String.Format("Insert Into MaterialDetalle(Material,Cantidad,MaterialId) Values('{0}','{1}',{2})", item.Material, item.Cantidad, Retornar));
                    }
                }

            }catch(Exception ex)
            {
                throw ex;
            }
            return Retornar >0;
        }

        public override bool Eliminar()
        {
            int retornar = 0;
            DbConexion cone = new DbConexion();
            try
            {
               retornar =Convert.ToInt16(cone.Ejecutar(String.Format("delete from Material where MaterialId ="+this.MaterialId)));

               if(retornar >0)
                {
                    cone.Ejecutar(String.Format("delete from MaterialDetalle where MaterialId =" + this.MaterialId));
                }

            }catch(Exception ex)
            {
                throw ex;
            }

            return retornar > 0;
        }

        public override DataTable Listar(string Campo, string Condicion, string Orden)
        {
            throw new NotImplementedException();
            DbConexion cone = new DbConexion();
            string OrdenFinal = "";
            if (!Orden.Equals(""))
                OrdenFinal = "Orden by" + Orden;
            return cone.ObtenerDatos("Select " + Campo + "from MaterialesDetalle where "+ Condicion + Orden);

            
        }
        public void AgregarMateriales(string Material,int Cantidad )
        {
            this.Detalle.Add(new MaterialesDetalle(Material, Cantidad));
        }
    }
}

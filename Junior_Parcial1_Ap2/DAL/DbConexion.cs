using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
   public class DbConexion
    {
        SqlConnection cone;
        SqlCommand coma;

        public DbConexion()
        {
            cone = new SqlConnection(ConfigurationManager.ConnectionStrings["DbArticulos"].ConnectionString);
            coma = new SqlCommand();
        }
        public bool Ejecutar(String CommaSql)
        {
            bool Retornar = false;
            try
            {
              cone.Open();
              coma.Connection = cone;
              coma.CommandText = CommaSql;
              coma.ExecuteNonQuery();
              Retornar = true;

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cone.Close();
            }

            return Retornar; 
        }
        public DataTable ObtenerDatos(String CommaSql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter;
            try
            {
                cone.Open();
                coma.Connection = cone;
                coma.CommandText = CommaSql;

                adapter = new SqlDataAdapter(coma);
                adapter.Fill(dt);

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cone.Close();
            }
            return dt;
        }
        public Object ObtenerValor(String CommaSql)
        {
            Object Retornar = null;
            try
            {
                cone.Open();
                coma.Connection = cone;
                coma.CommandText = CommaSql;

                Retornar = coma.ExecuteScalar();

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cone.Close();
            }
            return Retornar;
        }
    }
}

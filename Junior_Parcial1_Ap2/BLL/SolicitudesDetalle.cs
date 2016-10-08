using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SolicitudesDetalle
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int IdMaterial { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public string IdMateriales { get; set; }

        public SolicitudesDetalle(int IdSolicitud,int IdMateriales, int Cantidad, float Precio)
        {
            this.IdMaterial = IdMateriales;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.IdSolicitud = IdSolicitud;
        }
    }
}

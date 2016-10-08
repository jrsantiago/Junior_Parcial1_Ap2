using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class MaterialesDetalle
    {
        public int DetalleId { get; set; }
        public int  MaterialId { get; set; }
        public string Material { get; set; }
        public int  Cantidad { get; set; }
        public MaterialesDetalle(string Material,int Cantidad)
        {
            this.Material = Material;
            this.Cantidad = Cantidad;
        }

    }
}

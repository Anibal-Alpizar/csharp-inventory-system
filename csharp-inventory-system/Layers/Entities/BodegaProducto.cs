using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.Entities
{
    public class BodegaProducto
    {
        public int Id { get; set; }
        public int IdTipoBodega { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public double Precio{ get; set; }
        public DateTime Fecha { get; set; }
        public int InventarioInicial { get; set; }
        public int CantidadEntradas { get; set; }
        public int CantidadSalidas { get; set; }
        public int InventarioFinal { get; set; }
    }
}

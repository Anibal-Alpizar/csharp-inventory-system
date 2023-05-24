using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Interfaces.Bodega;
using csharp_inventory_system.Layers.DAL;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.BLL.Bodega
{
    public class BLLBodegaLimpieza : IBLLBodegaLimpieza
    {
        public List<BodegaProducto> GetAllProductosLimpieza()
        {
            IDALBodegaProducto _IDALProducto = new DALBodegaAlimentos();
            return _IDALProducto.GetAllProductosLimpieza();
        }
    }
}

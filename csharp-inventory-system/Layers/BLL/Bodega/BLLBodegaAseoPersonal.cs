using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.DAL;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.BLL.Bodega
{
    public class BLLBodegaAseoPersonal
    {
        public List<BodegaProducto> GetAllProductosAseoPersonal()
        {
            IDALBodegaProducto _IDALProducto = new DALBodegaAlimentos();
            return _IDALProducto.GetAllProductosAseoPersonal();
        }
    }
}

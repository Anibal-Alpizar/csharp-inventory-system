using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Interfaces.Bodega;
using csharp_inventory_system.Layers.DAL;
using csharp_inventory_system.Layers.DAL.Bodega;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.BLL.Bodega
{
    public class BLLBodegaAseoPersonal : IBLLBodegaAseoPersonal
    {
        public List<BodegaProducto> GetAllProductosAseoPersonal()
        {
            IDALBodegaAseoPersonal _IDALProducto = new DALBodegaAseoPersonal();
            return _IDALProducto.GetAllProductosAseoPersonal();
        }

        public Task<BodegaProducto> SaveBodegaAseoPersonal(BodegaProducto bodegaProducto)
        {
            IDALBodegaAseoPersonal _DALLBodegaAseoPersonal = new DALBodegaAseoPersonal();
            Task<BodegaProducto> oBodegaAseoPersonal = null;
            if (_DALLBodegaAseoPersonal.GetProductoAseoPersonalById(bodegaProducto.Nombre) == null)
                oBodegaAseoPersonal = _DALLBodegaAseoPersonal.SaveProductoAseoPersonal(bodegaProducto);
            else
                oBodegaAseoPersonal = _DALLBodegaAseoPersonal.UpdateProductoAseoPersonal(bodegaProducto);
            return oBodegaAseoPersonal;
        }
    }
}

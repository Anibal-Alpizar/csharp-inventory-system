using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.DAL;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.BLL
{
    public class BLLBodegaAlimentos : IBLLBodegaProducto
    {

        public List<BodegaProducto> GetAllProductos()
        {
            IDALBodegaProducto _IDALProducto = new DALBodegaAlimentos();
            return _IDALProducto.GetAllProductos();
        }

        public BodegaProducto SaveBodegaProducto(BodegaProducto pProduct)
        {
            IDALBodegaProducto _IDALProducto = new DALBodegaAlimentos();
            BodegaProducto oBodegaProducto = null;
            if (_IDALProducto.GetBodegaProductoById(pProduct.Nombre) != null)
                oBodegaProducto = _IDALProducto.SaveBodegaProducto(pProduct);
            // else
            // oBodegaProducto = _IDALProducto.UpdateBodegaProducto(pProduct);
            return oBodegaProducto;
        }
    }
}

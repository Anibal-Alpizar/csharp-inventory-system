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

namespace csharp_inventory_system.Layers.BLL
{
    public class BLLBodegaAlimentos : IBLLBodegaProducto
    {
        public Task<bool> DeleteBodegaAlimentos(string nombreProducto)
        {
            IDALBodegaAlimentos _DALLBodegaAlimentos = new DALBodegaAlimentos();
            return _DALLBodegaAlimentos.DeleteProductoAlimentos(nombreProducto);
        }

        public List<BodegaProducto> GetAllProductosAlimentos()
        {
            IDALBodegaAlimentos _IDALProductoAlimentos = new DALBodegaAlimentos();
            return _IDALProductoAlimentos.GetAllProductosAlimentos();
        }

        public Task<BodegaProducto> SaveBodegaAlimentos(BodegaProducto bodegaProducto)
        {
            IDALBodegaAlimentos _DALLBodegaAlimentos = new DALBodegaAlimentos();
            Task<BodegaProducto> oBodegaAlimentos = null;
            if (_DALLBodegaAlimentos.GetProductoAlimentosById(bodegaProducto.Nombre) == null)
                oBodegaAlimentos = _DALLBodegaAlimentos.SaveProductoAlimentos(bodegaProducto);
            else
                oBodegaAlimentos = _DALLBodegaAlimentos.UpdateProductoAlimentos(bodegaProducto);
            return oBodegaAlimentos;
        }
    }
}

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
    public class BLLBodegaLimpieza : IBLLBodegaLimpieza
    {
        public List<BodegaProducto> GetAllProductosLimpieza()
        {
            IDALBodegaLimpieza _IDALProducto = new DALBodegaLimpieza();
            return _IDALProducto.GetAllProductosLimpieza();
        }

        public Task<BodegaProducto> SaveBodegaLimpieza(BodegaProducto bodegaProducto)
        {
            IDALBodegaLimpieza _DALLBodegaLimpieza = new DALBodegaLimpieza();
            Task<BodegaProducto> oBodegaLimpieza = null;
            if (_DALLBodegaLimpieza.GetProductoLimpiezaById(bodegaProducto.Nombre) == null)
                oBodegaLimpieza = _DALLBodegaLimpieza.SaveProductoLimpieza(bodegaProducto);
            else
                oBodegaLimpieza = _DALLBodegaLimpieza.UpdateProductoLimpieza(bodegaProducto);
            return oBodegaLimpieza;
        }
    }
}

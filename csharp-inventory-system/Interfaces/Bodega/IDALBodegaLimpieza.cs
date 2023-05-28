using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces.Bodega
{
    public interface IDALBodegaLimpieza
    {
        List<BodegaProducto> GetAllProductosLimpieza();
        BodegaProducto GetProductoLimpiezaById (string nombre);
        Task<BodegaProducto> SaveProductoLimpieza(BodegaProducto pBodegaProducto);
        Task<BodegaProducto> UpdateProductoLimpieza(BodegaProducto pBodegaProducto);

    }
}

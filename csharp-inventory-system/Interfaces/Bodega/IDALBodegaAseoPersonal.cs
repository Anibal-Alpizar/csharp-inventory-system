using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces.Bodega
{
    public interface IDALBodegaAseoPersonal
    {
        List<BodegaProducto> GetAllProductosAseoPersonal();
        BodegaProducto GetProductoAseoPersonalById(string nombre);
        Task<BodegaProducto> SaveProductoAseoPersonal(BodegaProducto pBodegaProducto);
        Task<BodegaProducto> UpdateProductoAseoPersonal(BodegaProducto pBodegaProducto);
    }
}

using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces
{
    public interface IDALBodegaAlimentos
    {
        List<BodegaProducto> GetAllProductosAlimentos();
        BodegaProducto GetProductoAlimentosById(string nombre);
        Task<BodegaProducto> SaveProductoAlimentos(BodegaProducto pBodegaProducto);
        Task<BodegaProducto> UpdateProductoAlimentos(BodegaProducto pBodegaProducto);
        Task<bool> DeleteProductoAlimentos(string nombre);
    }
}

using csharp_inventory_system.Layers.Entities;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces.Bodega
{
    public interface IBLLBodegaLimpieza
    {
        List<BodegaProducto> GetAllProductosLimpieza();
        Task<BodegaProducto> SaveBodegaLimpieza(BodegaProducto bodegaProducto);
        Task<bool> DeleteBodegaProducto(string nombreProducto);
    }
}

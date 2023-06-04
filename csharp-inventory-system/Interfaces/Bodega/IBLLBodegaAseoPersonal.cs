using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces.Bodega
{
    public interface IBLLBodegaAseoPersonal
    {
        List<BodegaProducto> GetAllProductosAseoPersonal();
        Task<BodegaProducto> SaveBodegaAseoPersonal(BodegaProducto bodegaProducto);
        Task<bool> DeleteBodegaProducto(string nombreProducto);
    }
}

﻿using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Interfaces
{
    public interface IDALBodegaAlimentos
    {
        BodegaProducto GetBodegaProductoById(string pId);
        BodegaProducto SaveBodegaProducto(BodegaProducto pBodegaProducto);
        BodegaProducto UpdateBodegaProducto(BodegaProducto pBodegaProducto);
        List<BodegaProducto> GetAllProductos();


        //List<BodegaProducto> GetAllProductosLimpieza();
        //List<BodegaProducto> GetAllProductosAseoPersonal();
    }
}
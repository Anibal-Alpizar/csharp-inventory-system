﻿using csharp_inventory_system.Layers.Entities;
using csharp_inventory_system.Layers.Persistencia;
using csharp_inventory_system.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using csharp_inventory_system.Interfaces.Bodega;

namespace csharp_inventory_system.Layers.DAL.Bodega
{

    public class DALBodegaLimpieza : IDALBodegaLimpieza
    {

        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public List<BodegaProducto> GetAllProductosLimpieza()
        {
            DataSet ds = null;
            List<BodegaProducto> lista = new List<BodegaProducto>();
            SqlCommand command = new SqlCommand();
            string sql = @"SELECT  BodegaProducto.TipoBodega, BodegaProducto.Nombre, 
                            BodegaProducto.UnidadMedida ,BodegaProducto.InventarioInicial, BodegaProducto.Fecha
                            FROM BodegaProducto WHERE TipoBodega = 'Limpieza'";
            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BodegaProducto oBodegaProducto = new BodegaProducto()
                        {
                            TipoBodega = dr["TipoBodega"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            InventarioInicial = int.Parse(dr["InventarioInicial"].ToString()),
                            UnidadMedida = dr["UnidadMedida"].ToString(),
                            Fecha = DateTime.Parse(dr["Fecha"].ToString()),
                        };
                        lista.Add(oBodegaProducto);
                    }
                }
                return lista;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }
    }
}

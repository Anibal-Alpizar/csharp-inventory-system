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
using MySqlX.XDevAPI;

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

        public BodegaProducto GetProductoLimpiezaById(string nombre)
        {
            DataSet ds = null;
            BodegaProducto oBodegaProducto = null;
            SqlCommand command = new SqlCommand();

            try
            {
                string sql = @"SELECT * from BodegaProducto WHERE TipoBodega = 'Limpieza' AND Nombre = @Nombre";
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    oBodegaProducto = new BodegaProducto()
                    {
                        TipoBodega = dr["TipoBodega"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        InventarioInicial = int.Parse(dr["InventarioInicial"].ToString()),
                        UnidadMedida = dr["UnidadMedida"].ToString(),
                        Fecha = DateTime.Parse(dr["Fecha"].ToString()),
                    };
                }
                return oBodegaProducto;
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

        public async Task<BodegaProducto> SaveProductoLimpieza(BodegaProducto pBodegaProducto)
        {
            BodegaProducto oBodegaProducto = null;
            SqlCommand cmd = new SqlCommand();
            string sql = @"INSERT INTO BodegaProducto (TipoBodega, Nombre, UnidadMedida, Precio, Fecha ,InventarioInicial, CantidadEntradas, CantidadSalidas, CantidadFinal)
                            VALUES (@TipoBodega, @Nombre, @UnidadMedida, @Precio , @Fecha,@InventarioInicial, @CantidadEntrada, @CantidadSalida, @CantidadFinal)";
            try
            {
                cmd.Parameters.AddWithValue("@TipoBodega", pBodegaProducto.TipoBodega);
                cmd.Parameters.AddWithValue("@Nombre", pBodegaProducto.Nombre);
                cmd.Parameters.AddWithValue("@UnidadMedida", pBodegaProducto.UnidadMedida);
                cmd.Parameters.AddWithValue("@Precio", pBodegaProducto.Precio);
                cmd.Parameters.AddWithValue("@Fecha", pBodegaProducto.Fecha);
                cmd.Parameters.AddWithValue("@InventarioInicial", pBodegaProducto.InventarioInicial);
                cmd.Parameters.AddWithValue("@CantidadEntrada", pBodegaProducto.CantidadEntradas);
                cmd.Parameters.AddWithValue("@CantidadSalida", pBodegaProducto.CantidadSalidas);
                cmd.Parameters.AddWithValue("@CantidadFinal", pBodegaProducto.InventarioFinal);

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(cmd, IsolationLevel.ReadCommitted);

                    // Si devuelve filas quiere decir que se salvo entonces extraerlo
                    if (rows > 0)
                        oBodegaProducto = this.GetProductoLimpiezaById(pBodegaProducto.Nombre);
                }

                return oBodegaProducto;

            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), cmd, er as SqlException));
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

        public async Task<BodegaProducto> UpdateProductoLimpieza(BodegaProducto pBodegaProducto)
        {
            string sql = @"UPDATE BodegaProducto SET TipoBodega = @TipoBodega, Nombre = @Nombre, UnidadMedida = @UnidadMedida, Precio = @Precio, Fecha = @Fecha, InventarioInicial = @InventarioInicial, CantidadEntradas = @CantidadEntrada, CantidadSalidas = @CantidadSalida, CantidadFinal = @CantidadFinal
                            WHERE TipoBodega = 'Limpieza' AND Nombre = @Nombre";
            int rows = 0;
            SqlCommand cmd = new SqlCommand();
            BodegaProducto oBodegaProducto = new BodegaProducto();
            try
            {
                cmd.Parameters.AddWithValue("@TipoBodega", pBodegaProducto.TipoBodega);
                cmd.Parameters.AddWithValue("@Nombre", pBodegaProducto.Nombre);
                cmd.Parameters.AddWithValue("@UnidadMedida", pBodegaProducto.UnidadMedida);
                cmd.Parameters.AddWithValue("@Precio", pBodegaProducto.Precio);
                cmd.Parameters.AddWithValue("@Fecha", pBodegaProducto.Fecha);
                cmd.Parameters.AddWithValue("@InventarioInicial", pBodegaProducto.InventarioInicial);
                cmd.Parameters.AddWithValue("@CantidadEntrada", pBodegaProducto.CantidadEntradas);
                cmd.Parameters.AddWithValue("@CantidadSalida", pBodegaProducto.CantidadSalidas);
                cmd.Parameters.AddWithValue("@CantidadFinal", pBodegaProducto.InventarioFinal);
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(cmd, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oBodegaProducto = this.GetProductoLimpiezaById(pBodegaProducto.Nombre);

                return oBodegaProducto;

            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), cmd, er as SqlException));
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
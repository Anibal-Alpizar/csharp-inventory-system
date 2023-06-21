using csharp_inventory_system.Interfaces.Bodega;
using csharp_inventory_system.Layers.Entities;
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

namespace csharp_inventory_system.Layers.DAL.Bodega
{
    public class DALBodegaAseoPersonal : IDALBodegaAseoPersonal
    {
        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public async Task<bool> DeleteProductoLimpieza(string nombreProducto)
        {
            bool retorno = false;
            double rows = 0d;
            SqlCommand command = new SqlCommand();
            string sql = @"DELETE FROM BodegaProducto WHERE TipoBodega = 'Aseo_Personal' AND Nombre = @Nombre";
            try
            {
                command.Parameters.AddWithValue("@Nombre", nombreProducto);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }
                if (rows > 0)
                    retorno = true;
                return retorno;
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

        public List<BodegaProducto> GetAllProductosAseoPersonal()
        {
            DataSet ds = null;
            List<BodegaProducto> lista = new List<BodegaProducto>();
            SqlCommand command = new SqlCommand();
            string sql = @"SELECT  BodegaProducto.TipoBodega, BodegaProducto.Nombre, 
                            BodegaProducto.UnidadMedida ,BodegaProducto.InventarioInicial, BodegaProducto.Fecha,
                            BodegaProducto.Precio, BodegaProducto.CantidadEntradas, BodegaProducto.CantidadSalidas
                            FROM BodegaProducto WHERE TipoBodega = 'Aseo_Personal'";
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
                            Precio = double.Parse(dr["Precio"].ToString()),
                            CantidadEntradas = int.Parse(dr["CantidadEntradas"].ToString()),
                            CantidadSalidas = int.Parse(dr["CantidadSalidas"].ToString()),
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

        public BodegaProducto GetProductoAseoPersonalById(string nombre)
        {
            DataSet ds = null;
            BodegaProducto oBodegaProducto = null;
            SqlCommand command = new SqlCommand();
            string sql = @"SELECT  BodegaProducto.TipoBodega, BodegaProducto.Nombre, 
                            BodegaProducto.UnidadMedida ,BodegaProducto.InventarioInicial, BodegaProducto.Fecha
                            FROM BodegaProducto WHERE TipoBodega = 'Aseo_Personal' AND Nombre = @Nombre";
            try
            {
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

        public async Task<BodegaProducto> SaveProductoAseoPersonal(BodegaProducto pBodegaProducto)
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
                        oBodegaProducto = this.GetProductoAseoPersonalById(pBodegaProducto.Nombre);
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

        public async Task<BodegaProducto> UpdateProductoAseoPersonal(BodegaProducto pBodegaProducto)
        {
            string sql = @"UPDATE BodegaProducto SET TipoBodega = @TipoBodega, Nombre = @Nombre, UnidadMedida = @UnidadMedida, InventarioInicial = @InventarioInicial, Fecha = @Fecha WHERE Nombre = @Nombre";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@TipoBodega", pBodegaProducto.TipoBodega);
                cmd.Parameters.AddWithValue("@Nombre", pBodegaProducto.Nombre);
                cmd.Parameters.AddWithValue("@UnidadMedida", pBodegaProducto.UnidadMedida);
                cmd.Parameters.AddWithValue("@InventarioInicial", pBodegaProducto.InventarioInicial);
                cmd.Parameters.AddWithValue("@Fecha", pBodegaProducto.Fecha);
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(cmd, IsolationLevel.ReadCommitted);
                }
                return pBodegaProducto;
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

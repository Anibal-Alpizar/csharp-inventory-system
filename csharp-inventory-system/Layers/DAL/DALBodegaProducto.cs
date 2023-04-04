using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.Entities;
using csharp_inventory_system.Layers.Persistencia;
using csharp_inventory_system.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.DAL
{
    public class DALBodegaProducto : IDALBodegaProducto
    {
        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public List<BodegaProducto> GetAllProductos()
        {
            DataSet ds = null;
            List<BodegaProducto> lista = new List<BodegaProducto>();
            SqlCommand command = new SqlCommand();
            string sql = @"SELECT BodegaProducto.TipoBodega, BodegaProducto.Nombre, BodegaProducto.CantidadFinal FROM BodegaProducto WHERE TipoBodega = 'Alimentos'";
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
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        BodegaProducto oBodegaProducto = new BodegaProducto()
                        {
                            TipoBodega = dr["TipoBodega"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            InventarioFinal = int.Parse(dr["CantidadFinal"].ToString())
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

        public BodegaProducto GetBodegaProductoById(double pId)
        {
            DataSet ds = null;
            BodegaProducto oBodegaProducto = null;
            string sql = @" select * from [BodegaProducto] where IdBodegaProducto = @IdBodegaProducto";
            SqlCommand command = new SqlCommand();
            try
            {
                command.Parameters.AddWithValue("@IdBodegaProducto", pId);
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
                        IdBodegaProducto = double.Parse(dr["IdBodegaProducto"].ToString()),
                        TipoBodega = dr["TipoBodega"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        UnidadMedida = dr["UnidadMedida"].ToString(),
                        Precio = double.Parse(dr["Precio"].ToString()),
                        Fecha = DateTime.Parse(dr["Fecha"].ToString()),
                        InventarioInicial = int.Parse(dr["InventarioInicial"].ToString()),
                        CantidadEntradas = int.Parse(dr["CantidadEntradas"].ToString()),
                        CantidadSalidas = int.Parse(dr["CantidadSalidas"].ToString()),
                        InventarioFinal = int.Parse(dr["CantidadFinal"].ToString())
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

        public BodegaProducto SaveBodegaProducto(BodegaProducto pBodegaProducto)
        {
            BodegaProducto oBodegaProducto = null;
            string sql = @"INSERT INTO [dbo].[BodegaProducto]
                                  ([IdBodegaProducto],
                                  [TipoBodega],
                                  [Nombre],
                                  [UnidadMedida],
                                  [Precio],
                                  [Fecha],
                                  [InventarioInicial],
                                  [CantidadEntradas],
                                  [CantidadSalidas],
                                  [CantidadFinal]
                                  )
                                VALUES
                                  (@IdBodegaProducto, 
                                   @TipoBodega,
                                   @Nombre,
                                   @UnidadMedida,
                                   @Precio,
                                   @Fecha,
                                   @InventarioInicial,
                                   @CantidadEntradas,
                                   @CantidadSalidas,
                                   @CantidadFinal)";
            
            SqlCommand command = new SqlCommand();
            double rows = 0;
            try
            {
                command.Parameters.AddWithValue("@IdBodegaProducto", pBodegaProducto.IdBodegaProducto);
                command.Parameters.AddWithValue("@TipoBodega", pBodegaProducto.TipoBodega);
                command.Parameters.AddWithValue("@Nombre", pBodegaProducto.Nombre);
                command.Parameters.AddWithValue("@UnidadMedida", pBodegaProducto.UnidadMedida);
                command.Parameters.AddWithValue("@Precio", pBodegaProducto.Precio);
                command.Parameters.AddWithValue("@Fecha", pBodegaProducto.Fecha);
                command.Parameters.AddWithValue("@InventarioInicial", pBodegaProducto.InventarioInicial);
                command.Parameters.AddWithValue("@CantidadEntradas", pBodegaProducto.CantidadEntradas);
                command.Parameters.AddWithValue("@CantidadSalidas", pBodegaProducto.CantidadSalidas);
                command.Parameters.AddWithValue("@CantidadFinal", pBodegaProducto.InventarioFinal);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
                if (rows > 0)
                {
                    oBodegaProducto = GetBodegaProductoById(pBodegaProducto.IdBodegaProducto);
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

        public BodegaProducto UpdateBodegaProducto(BodegaProducto pBodegaProducto)
        {
            throw new NotImplementedException();
        }
    }
}

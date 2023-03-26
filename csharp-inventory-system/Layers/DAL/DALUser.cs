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
    public class DALUser : IDALUser
    {
        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public User Login(string pLogin, string pPassword)
        {
            StringBuilder conecction = new StringBuilder();
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            User oUser = null;
            try
            {
                command.CommandText = @"select * from Usuario with (rowlock) where Login = @pLogin and Password = @pPassword";
                command.Parameters.AddWithValue("@pLogin", pLogin);
                command.Parameters.AddWithValue("@pPassword", pPassword);
                command.CommandType = CommandType.Text;
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUser = new User();
                        oUser.Login = reader["Login"].ToString();
                        oUser.IdRol = int.Parse(reader["IdRol"].ToString());
                        oUser.Password = reader["Password"].ToString();
                        oUser.Nombre = reader["Nombre"].ToString();
                        oUser.Estado = bool.Parse(reader["Estado"].ToString());
                    }
                }
                return oUser;
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

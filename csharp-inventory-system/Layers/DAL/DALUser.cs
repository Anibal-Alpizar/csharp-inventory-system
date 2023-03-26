using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.DAL
{
    public class DALUser : IDALUser
    {
        public User Login(string pLogin, string pPassword)
        {
            StringBuilder conecction = new StringBuilder();
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            User oUser = null;
            try
            {

            }
        }
    }
}

using csharp_inventory_system.Interfaces;
using csharp_inventory_system.Layers.DAL;
using csharp_inventory_system.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.BLL
{
    public class BLLUser : IBLLUser
    {
        public User Login(string pLogin, string pPassword)
        {
            IDALUser _DALUser = new DALUser();
            return _DALUser.Login(pLogin, pPassword);
        }
    }
}

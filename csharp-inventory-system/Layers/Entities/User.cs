using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Layers.Entities
{
    public class User
    {
        public string Login { get; set; }
        public int IdRol { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}

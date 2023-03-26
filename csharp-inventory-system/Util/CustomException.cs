using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_inventory_system.Util
{
    [Serializable]
    class CustomException : Exception
    {
        public CustomException()
        {
        }
        public CustomException(string pParametro)
            : base(pParametro)
        {
        }
    }
}

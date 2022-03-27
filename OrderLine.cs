using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegrammBot3
{
    public class OrderLine
    {
        public Product product { get; set; }
        public decimal qty { get; set; }

        public override string ToString()
        {
            return $"{product.NameProduct} - {qty}";
        }
    }
}

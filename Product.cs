using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegrammBot3
{
    public class Product
    {
        public string NameProduct { get; set; }
        public decimal price { get; set; }
        public string Unit { get; set; }

        public override string ToString()
        {
            return $" {NameProduct} {price} {Unit} ";
        }
    }
}

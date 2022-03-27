using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegrammBot3
{
    public class Order
    {
        public long chatId { get; set; }
        public OrderState state { get; set; }
        public string City { get; set; }
        public List<OrderLine> products { get; set; } = new List<OrderLine>();
        public string Adress { get; set; }


        public decimal OrderPrice
        {
            get
            {
                decimal allprice = 0;

                for (int i = 0; i < products.Count; i++)
                {
                    allprice += products[i].qty * products[i].product.price;
                }

                return allprice;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < products.Count; i++)
            {
                sb.AppendLine($"    {products[i].product.NameProduct} - {products[i].qty} {products[i].product.Unit} ");
            }

            sb.AppendLine($" Total price = {OrderPrice} ");
            sb.AppendLine($"City: {City} ");
            sb.AppendLine($"Adress: {Adress} ");

            return sb.ToString();
        }
    }

    public enum OrderState { start, goods, city, adress, end }
}

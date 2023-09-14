using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class DiscountProduct : Product
    {
        public double Discount { get; set; }

        public DiscountProduct(int productId, string name, double price, double discount)
            : base(productId, name, price)
        {
            Discount = discount;
        }
    }
}

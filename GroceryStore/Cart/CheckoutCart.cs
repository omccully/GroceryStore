using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class CheckoutCart
    {
        public List<IGroceryItemOrder> Orders { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Orders.Sum(order => order.Price);
            }
        }

        public CheckoutCart()
        {
            Orders = new List<IGroceryItemOrder>();
        }
    }
}

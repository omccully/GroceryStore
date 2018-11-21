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

        public IEnumerable<IGroceryItemOrder> OrdersCombinedByItem
        {
            get
            {
                return Orders.GroupBy(order => order.Item).Select(CombineOrders);
            }
        }

        IGroceryItemOrder CombineOrders(IEnumerable<IGroceryItemOrder> orders)
        {
            return orders.Aggregate((accumulation, order) => accumulation.Combine(order));
        }
    }
}

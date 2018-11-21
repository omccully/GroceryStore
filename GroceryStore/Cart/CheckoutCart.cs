using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    /// <summary>
    /// Contains a collection of IGroceryItemOrders that is used to caclculate the total price.
    /// </summary>
    public class CheckoutCart
    {
        public List<IGroceryItemOrder> Orders { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return OrdersCombinedByItem.Sum(order => order.Price);
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

        public IGroceryItemOrder CombineOrdersForItem(IGroceryItem item)
        {
            return OrdersCombinedByItem.First(combinedOrder => combinedOrder.Item == item);
        }
    }
}

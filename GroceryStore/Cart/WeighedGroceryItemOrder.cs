using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class WeighedGroceryItemOrder
    {
        public WeighedGroceryItem Item { get; private set; }
        public decimal Weight { get; set; }

        public WeighedGroceryItemOrder(WeighedGroceryItem item, decimal weight)
        {
            this.Item = item;
            this.Weight = weight;
        }

    }
}

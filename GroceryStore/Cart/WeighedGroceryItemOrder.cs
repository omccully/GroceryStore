using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class WeighedGroceryItemOrder : GroceryItemOrder<decimal>
    {
        public decimal Weight => Quantity;

        public WeighedGroceryItemOrder(IGroceryItem<decimal> item, decimal weight)
            : base(item, weight)
        {
        }

        public override IGroceryItemOrder Combine(IGroceryItemOrder otherOrder)
        {
            return Combine((WeighedGroceryItemOrder)otherOrder);
        }

        public WeighedGroceryItemOrder Combine(WeighedGroceryItemOrder otherOrder)
        {
            if (Item != otherOrder.Item)
                throw new DifferingItemsException();

            return new WeighedGroceryItemOrder(Item, Weight + otherOrder.Weight);
        }
    }
}

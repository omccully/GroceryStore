using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;
using GroceryStore.Cart.OrderFactories;

namespace GroceryStore.Cart
{
    public class EachesGroceryItemOrder : GroceryItemOrder<int>
    {
        public int Count => Quantity;

        public EachesGroceryItemOrder(IGroceryItem<int> item, int count = 1)
            : base(item, count)
        { 
        }

        public override IGroceryItemOrder Combine(IGroceryItemOrder otherOrder)
        {
            return Combine((EachesGroceryItemOrder)otherOrder);
        }

        public EachesGroceryItemOrder Combine(EachesGroceryItemOrder otherOrder)
        {
            if (Item != otherOrder.Item)
                throw new DifferingItemsException();

            return new EachesGroceryItemOrder(Item, Count + otherOrder.Count);
        }
    }
}

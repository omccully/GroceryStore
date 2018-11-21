using GroceryStore.Cart;
using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreTests.Cart
{
    class GroceryItemOrderFake : IGroceryItemOrder
    {
        public decimal Price { get; private set; }

        public IGroceryItem Item { get; private set; }

        public GroceryItemOrderFake(decimal price)
        {
            this.Price = price;
        }

        public GroceryItemOrderFake(IGroceryItem item, decimal price)
        {
            this.Item = item;
            this.Price = price;
        }

        public IGroceryItemOrder Combine(IGroceryItemOrder otherOrder)
        {
            return new GroceryItemOrderFake(Price + otherOrder.Price);
        }
    }
}

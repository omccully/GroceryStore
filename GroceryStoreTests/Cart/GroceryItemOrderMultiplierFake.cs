using GroceryStore.Cart;
using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreTests.Cart
{
    class GroceryItemOrderMultiplierFake : IGroceryItemOrder
    {
        public decimal Price { get; private set; }

        public IGroceryItem Item { get; private set; }

        public GroceryItemOrderMultiplierFake(IGroceryItem item, decimal price)
        {
            this.Item = item;
            this.Price = price;
        }

        public IGroceryItemOrder Combine(IGroceryItemOrder otherOrder)
        {
            return new GroceryItemOrderMultiplierFake(Item, Price * otherOrder.Price);
        }
    }
}

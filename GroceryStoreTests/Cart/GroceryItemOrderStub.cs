using GroceryStore.Cart;
using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreTests.Cart
{
    class GroceryItemOrderStub : IGroceryItemOrder
    {
        public decimal Price { get; private set; }

        public IGroceryItem Item => throw new NotImplementedException();

        public GroceryItemOrderStub(decimal price)
        {
            this.Price = price;
        }
    }
}

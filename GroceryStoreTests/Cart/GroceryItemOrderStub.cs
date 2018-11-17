using GroceryStore.Cart;
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

        public GroceryItemOrderStub(decimal price)
        {
            this.Price = price;
        }
    }
}

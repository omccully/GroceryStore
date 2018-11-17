using System;
using GroceryStore.Cart;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class GroceryCartTests
    {
        [TestMethod]
        public void Orders_CanAddEachesItem()
        {
            EachesGroceryItemOrder eachesOrder = new EachesGroceryItemOrder(null, 3);

            GroceryCart cart = new GroceryCart();

            Assert.AreEqual(0, cart.Orders.Count);
            cart.Orders.Add(eachesOrder);
            Assert.AreEqual(1, cart.Orders.Count);
        }

        [TestMethod]
        public void Orders_CanAddWeighedItem()
        {
            WeighedGroceryItemOrder weighedOrder = new WeighedGroceryItemOrder(null, 3M);

            GroceryCart cart = new GroceryCart();

            Assert.AreEqual(0, cart.Orders.Count);
            cart.Orders.Add(weighedOrder);
            Assert.AreEqual(1, cart.Orders.Count);
        }
    }
}

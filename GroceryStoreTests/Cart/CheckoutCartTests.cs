using System;
using GroceryStore.Cart;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class CheckoutCartTests
    {
        [TestMethod]
        public void Orders_CanAddEachesItem()
        {
            EachesGroceryItemOrder eachesOrder = new EachesGroceryItemOrder(null, 3);

            CheckoutCart cart = new CheckoutCart();

            Assert.AreEqual(0, cart.Orders.Count);
            cart.Orders.Add(eachesOrder);
            Assert.AreEqual(1, cart.Orders.Count);
        }

        [TestMethod]
        public void Orders_CanAddWeighedItem()
        {
            WeighedGroceryItemOrder weighedOrder = new WeighedGroceryItemOrder(null, 3M);

            CheckoutCart cart = new CheckoutCart();

            Assert.AreEqual(0, cart.Orders.Count);
            cart.Orders.Add(weighedOrder);
            Assert.AreEqual(1, cart.Orders.Count);
        }

        [TestMethod]
        public void Price_CalculatesTotalPriceFromOrders()
        {
            CheckoutCart cart = new CheckoutCart();
            cart.Orders.Add(new GroceryItemOrderFake(1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(2.00M));

            Assert.AreEqual(3.50M, cart.TotalPrice);
        }

        /*[TestMethod]
        public void GroupOrdersByItem_GroupsOrdersByItem()
        {
            CheckoutCart cart = new CheckoutCart();

            //cart.Orders.Add()
            Assert.Fail();
        }*/
    }
}

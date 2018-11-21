using System;
using GroceryStore.Cart;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestMethod]
        public void CombineOrdersForItem_GroupsOrderByItem()
        {
            IGroceryItem itemA = new Mock<IGroceryItem>().Object;
            IGroceryItem itemB = new Mock<IGroceryItem>().Object;

            CheckoutCart cart = new CheckoutCart();

            cart.Orders.Add(new GroceryItemOrderFake(itemA, 1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemA, 3.50M));

            Assert.AreEqual(5.00M, cart.CombineOrdersForItem(itemA).Price);
            Assert.AreEqual(6.50M, cart.CombineOrdersForItem(itemB).Price);
        }

        [TestMethod]
        public void CombineOrdersForItem_ThrowsException_IfItemDoesntExist()
        {
            IGroceryItem itemA = new Mock<IGroceryItem>().Object;
            IGroceryItem itemB = new Mock<IGroceryItem>().Object;
            IGroceryItem unusedItem = new Mock<IGroceryItem>().Object;

            CheckoutCart cart = new CheckoutCart();

            cart.Orders.Add(new GroceryItemOrderFake(itemA, 1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemA, 3.50M));

            Assert.ThrowsException<InvalidOperationException>(() => 
               cart.CombineOrdersForItem(unusedItem));
        }

        [TestMethod]
        public void CombineOrdersForItem_ThrowsException_IfNoItemsAdded()
        {
            IGroceryItem itemA = new Mock<IGroceryItem>().Object;
            IGroceryItem itemB = new Mock<IGroceryItem>().Object;
            IGroceryItem unusedItem = new Mock<IGroceryItem>().Object;

            CheckoutCart cart = new CheckoutCart();

            cart.Orders.Add(new GroceryItemOrderFake(itemA, 1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemA, 3.50M));

            Assert.ThrowsException<InvalidOperationException>(() =>
               cart.CombineOrdersForItem(unusedItem));
        }
    }
}

using System;
using System.Linq;
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
        public void TotalPrice_CalculatesTotalPriceFromOrders()
        {
            CheckoutCart cart = new CheckoutCart();
            cart.Orders.Add(new GroceryItemOrderFake(1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(2.00M));

            Assert.AreEqual(3.50M, cart.TotalPrice);
        }

        [TestMethod]
        public void TotalPrice_UsesCombinesOrders()
        {
            IGroceryItem itemA = new Mock<IGroceryItem>().Object;
            IGroceryItem itemB = new Mock<IGroceryItem>().Object;

            CheckoutCart cart = new CheckoutCart();
            cart.Orders.Add(new GroceryItemOrderMultiplierFake(itemA, 2.00M));
            cart.Orders.Add(new GroceryItemOrderMultiplierFake(itemB, 3.00M));
            cart.Orders.Add(new GroceryItemOrderMultiplierFake(itemB, 3.00M));
            cart.Orders.Add(new GroceryItemOrderMultiplierFake(itemB, 3.00M));
            cart.Orders.Add(new GroceryItemOrderMultiplierFake(itemA, 2.00M));

            // when the order fakes are combined, the prices should be multiplied
            // A should be 2*2 = 4
            // B should be 3*3*3 = 27
            // 4 + 27 = 31

            Assert.AreEqual(31.00M, cart.TotalPrice);
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

        [TestMethod]
        public void OrdersCombinedByItem_CombinesOrders()
        {
            IGroceryItem itemA = new Mock<IGroceryItem>().Object;
            IGroceryItem itemB = new Mock<IGroceryItem>().Object;

            CheckoutCart cart = new CheckoutCart();

            cart.Orders.Add(new GroceryItemOrderFake(itemA, 1.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.50M));
            cart.Orders.Add(new GroceryItemOrderFake(itemB, 2.00M));
            cart.Orders.Add(new GroceryItemOrderFake(itemA, 3.50M));

            Assert.AreEqual(2, cart.OrdersCombinedByItem.Count());

            foreach(IGroceryItemOrder order in cart.OrdersCombinedByItem)
            {
                if(order.Item == itemA)
                {
                    Assert.AreEqual(5.00M, order.Price);
                }
                else if(order.Item == itemB)
                {
                    Assert.AreEqual(6.50M, order.Price);
                }
            }
        }
    }
}

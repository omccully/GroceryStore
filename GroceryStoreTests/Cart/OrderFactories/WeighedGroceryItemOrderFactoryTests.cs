﻿using System;
using GroceryStore.Cart;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Cart.OrderFactories;
using Moq;

namespace GroceryStoreTests.Cart.OrderFactories
{
    [TestClass]
    public class WeighedGroceryItemOrderFactoryTests
    {
        [TestMethod]
        public void CreateOrder_CreatesOrderWithNoWeight_IfIsIWeighedGroceryItem()
        {
            WeighedGroceryItem item = new WeighedGroceryItem("bananas", 2.38M);

            WeighedGroceryItemOrderFactory factory =
                new WeighedGroceryItemOrderFactory();

            IGroceryItemOrder order = factory.CreateOrder(item);
            Assert.IsTrue(order is WeighedGroceryItemOrder);
            WeighedGroceryItemOrder weighedOrder = ((WeighedGroceryItemOrder)order);
            Assert.AreEqual(item, weighedOrder.Item);
            Assert.AreEqual(0.0M, weighedOrder.Weight);
        }

        [TestMethod]
        public void CreateOrder_ThrowsException_IfIsNotEachesGroceryItem()
        {
            Mock<IGroceryItem> genericItemMock = new Mock<IGroceryItem>();

            WeighedGroceryItemOrderFactory factory =
                new WeighedGroceryItemOrderFactory();

            Assert.ThrowsException<InvalidGroceryItemTypeException>(() =>
            factory.CreateOrder(genericItemMock.Object));
        }

        [TestMethod]
        public void CreateOrder_CreatesOrderWithFiveItems_WhenFactoryCreatedWithCustomDefault()
        {
            WeighedGroceryItem item = new WeighedGroceryItem("bananas", 2.38M);

            WeighedGroceryItemOrderFactory factory =
                new WeighedGroceryItemOrderFactory(5);

            IGroceryItemOrder order = factory.CreateOrder(item);
            Assert.IsTrue(order is WeighedGroceryItemOrder);
            WeighedGroceryItemOrder weighedOrder = ((WeighedGroceryItemOrder)order);
            Assert.AreEqual(item, weighedOrder.Item);
            Assert.AreEqual(5, weighedOrder.Weight);
        }
    }
}

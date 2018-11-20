using System;
using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Cart.OrderFactories.CountSelectors;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Cart.OrderFactories
{
    [TestClass]
    public class EachesGroceryItemOrderFactoryTests
    {
        [TestMethod]
        public void CreateOrder_CreatesOrderWithOneItem_IfIsIEachesGroceryItem()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            EachesGroceryItemOrderFactory factory =
                new EachesGroceryItemOrderFactory();

            IGroceryItemOrder order = factory.CreateOrder(item);
            Assert.IsTrue(order is EachesGroceryItemOrder);
            EachesGroceryItemOrder eachesOrder = ((EachesGroceryItemOrder)order);
            Assert.AreEqual(item, eachesOrder.Item);
            Assert.AreEqual(1, eachesOrder.Count);
        }

        [TestMethod]
        public void CreateOrder_ThrowsException_IfIsNotEachesGroceryItem()
        {
            Mock<IGroceryItem> genericItemMock = new Mock<IGroceryItem>();

            EachesGroceryItemOrderFactory factory =
                new EachesGroceryItemOrderFactory();

            Assert.ThrowsException<InvalidGroceryItemTypeException>(() =>
            factory.CreateOrder(genericItemMock.Object));
        }

        [TestMethod]
        public void CreateOrder_CreatesOrderWithFiveItems_WhenFactoryCreatedWithCustomDefault()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            EachesGroceryItemOrderFactory factory =
                new EachesGroceryItemOrderFactory(5);

            IGroceryItemOrder order = factory.CreateOrder(item);
            Assert.IsTrue(order is EachesGroceryItemOrder);
            EachesGroceryItemOrder eachesOrder = ((EachesGroceryItemOrder)order);
            Assert.AreEqual(item, eachesOrder.Item);
            Assert.AreEqual(5, eachesOrder.Count);
        }

        [TestMethod]
        public void CreateOrder_GetsCountFromCountSelector_WhenFactoryCreatedWithCountSelector()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            Mock<ICountSelector> countSelectorMock = new Mock<ICountSelector>();
            countSelectorMock.Setup(cs => cs.SelectCount()).Returns(9);

            EachesGroceryItemOrderFactory factory =
                new EachesGroceryItemOrderFactory(countSelectorMock.Object);

            IGroceryItemOrder order = factory.CreateOrder(item);
            Assert.IsTrue(order is EachesGroceryItemOrder);
            EachesGroceryItemOrder eachesOrder = ((EachesGroceryItemOrder)order);
            Assert.AreEqual(item, eachesOrder.Item);
            Assert.AreEqual(9, eachesOrder.Count);
        }
    }
}

using System;
using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Cart.OrderFactories.WeightSelectors;
using GroceryStore.Stock;
using GroceryStore.Stock.Scanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Integration
{
    [TestClass]
    public class GroceryItemScannerIntegrationTests
    {
        [TestMethod]
        public void CreateOrder_GetsWeightFromWeightSelector()
        {
            decimal expectedWeight = 1.56M;

            string itemName = "bananas";
            WeighedGroceryItem bananas = new WeighedGroceryItem(itemName, 2.38M);

            Mock<IWeightSelector> weightSelectorMock = new Mock<IWeightSelector>();
            weightSelectorMock.Setup((ws) => ws.SelectWeight(bananas)).Returns(expectedWeight);

            IGroceryItemOrderFactory orderFactory = new AggregateGroceryItemOrderFactory(
                    new EachesGroceryItemOrderFactory(),
                    new WeighedGroceryItemOrderFactory(weightSelectorMock.Object)
                );

            GroceryItemScanner scanner = new GroceryItemScanner(orderFactory);
            scanner.Items.Add(bananas);

            WeighedGroceryItemOrder bananaOrder = (WeighedGroceryItemOrder)scanner.CreateOrder(itemName);

            Assert.AreEqual(expectedWeight, bananaOrder.Weight);
        }

        [TestMethod]
        public void CreateOrder_DefaultsToOneForEachesItems()
        {
            IGroceryItemOrderFactory orderFactory = new AggregateGroceryItemOrderFactory(
                    new EachesGroceryItemOrderFactory(),
                    new WeighedGroceryItemOrderFactory()
                );

            string itemName = "soup";

            GroceryItemScanner scanner = new GroceryItemScanner(orderFactory);
            scanner.Items.Add(new EachesGroceryItem(itemName, 1.89M));

            EachesGroceryItemOrder soupOrder = (EachesGroceryItemOrder)scanner.CreateOrder(itemName);

            Assert.AreEqual(1, soupOrder.Count);
        }

        [TestMethod]
        public void CreateOrder_ThrowsException_WhenUnknownItemType()
        {
            IGroceryItemOrderFactory orderFactory = new AggregateGroceryItemOrderFactory(
                    new EachesGroceryItemOrderFactory(),
                    new WeighedGroceryItemOrderFactory()
                );

            string itemName = "soup";

            Mock<IGroceryItem> groceryItemMock = new Mock<IGroceryItem>();
            groceryItemMock.Setup((item) => item.Name).Returns(itemName);
            GroceryItemScanner scanner = new GroceryItemScanner(orderFactory);
            scanner.Items.Add(groceryItemMock.Object);

            Assert.ThrowsException<InvalidGroceryItemTypeException>(() => scanner.CreateOrder(itemName));
        }
    }
}

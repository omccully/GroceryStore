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

            Mock<IWeightSelector> weightSelectorMock = new Mock<IWeightSelector>();
            weightSelectorMock.Setup((ws) => ws.SelectWeight()).Returns(expectedWeight);

            IGroceryItemOrderFactory orderFactory = new AggregateGroceryItemOrderFactory(
                    new EachesGroceryItemOrderFactory(),
                    new WeighedGroceryItemOrderFactory(weightSelectorMock.Object)
                );

            GroceryItemScanner scanner = new GroceryItemScanner(orderFactory);
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));

            WeighedGroceryItemOrder bananaOrder = (WeighedGroceryItemOrder)scanner.CreateOrder("bananas");

            Assert.AreEqual(expectedWeight, bananaOrder.Weight);
        }
    }
}

using System;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Cart;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class WeighedGroceryItemOrderTests
    {
        [TestMethod]
        public void Constructor_SetsItemAndWeight()
        {
            decimal weight = 1.6M;
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(bananas, weight);

            Assert.AreEqual(bananas, order.Item);
            Assert.AreEqual(weight, order.Weight);
        }

        [TestMethod]
        public void Price_CalculatesUsingItemMethod()
        {
            decimal weight = 1.6M;
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(bananas, weight);

            // 1.6 * 2.3 = 3.68

            Assert.AreEqual(3.68M, order.Price);
        }
    }
}

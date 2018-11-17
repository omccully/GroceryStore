using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;

namespace GroceryStoreTests.Stock
{
    [TestClass]
    public class WeighedGroceryItemTests
    {
        [TestMethod]
        public void Constructor_SetsNameAndOriginalPrice()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.38M);

            Assert.AreEqual("bananas", bananas.Name);
            Assert.AreEqual(2.38M, bananas.OriginalPrice);
        }
    }
}

using System;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStoreTests.Stock
{
    [TestClass]
    public class GroceryItemScannerTests
    {
        [TestMethod]
        public void Items_CanAddEachesGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new EachesGroceryItem("soup", 1.89M));
            Assert.AreEqual(1, scanner.Items.Count);
        }

        [TestMethod]
        public void Items_CanAddWeighedGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));
            Assert.AreEqual(1, scanner.Items.Count);
        }
    }
}

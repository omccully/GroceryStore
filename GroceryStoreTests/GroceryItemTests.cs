using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;

namespace GroceryStoreTests
{
    [TestClass]
    public class GroceryItemTests
    {
        [TestMethod]
        public void EachesConstructor_SetsNameAndOriginalPrice()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            Assert.AreEqual("soup", item.Name);
            Assert.AreEqual(1.89M, item.OriginalPrice);
        }

        [TestMethod]
        public void EachesMarkdownPrice_ReducesPurchasePrice()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            item.Markdown = 0.20M;

            Assert.AreEqual(1.69M, item.PurchasePrice);
        }

    }
}

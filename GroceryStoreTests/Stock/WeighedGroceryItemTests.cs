using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStoreTests.Markdowns;

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
            Assert.AreEqual(2.38M, bananas.OriginalPricePerUnit);
        }

        [TestMethod]
        public void PurchasePrice_GetsResultFromMarkdown()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.38M)
            {
                Markdown = new PriceMarkdownStub(1.50M)
            };

            Assert.AreEqual(1.50M, bananas.PurchasePricePerUnit);
        }
        
        [TestMethod]
        public void PurchasePrice_UsesOriginalPrice_WhenNoMarkdown()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.38M);

            Assert.AreEqual(2.38M, bananas.PurchasePricePerUnit);
        }
    }
}

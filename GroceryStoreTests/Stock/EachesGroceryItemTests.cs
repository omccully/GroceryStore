using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStoreTests.Markdowns;
using GroceryStoreTests.Specials.Eaches;

namespace GroceryStoreTests.Stock
{
    [TestClass]
    public class EachesGroceryItemTests
    {
        [TestMethod]
        public void Constructor_SetsNameAndOriginalPrice()
        {
            decimal expectedPrice = 1.89M;
            EachesGroceryItem item = new EachesGroceryItem("soup", expectedPrice);

            Assert.AreEqual("soup", item.Name);
            Assert.AreEqual(expectedPrice, item.OriginalPrice);
        }

        [TestMethod]
        public void PurchasePrice_GetsResultFromMarkdown()
        {
            decimal expectedPrice = 1.50M;
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(expectedPrice)
            };

            Assert.AreEqual(expectedPrice, item.PurchasePrice);
        }

        [TestMethod]
        public void PurchasePrice_UsesOriginalPrice_WhenNoMarkdown()
        {
            decimal expectedPrice = 1.89M;
            EachesGroceryItem item = new EachesGroceryItem("soup", expectedPrice);

            Assert.AreEqual(expectedPrice, item.PurchasePrice);
        }

        [TestMethod]
        public void CalculatePurchasePrice_AppliesSpecial_IfSpecialIsSet()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Special = new EachesGroceryItemSpecialFake(1.00M)
            };

            Assert.AreEqual(5.00M, item.CalculatePurchasePrice(5));
        }

        [TestMethod]
        public void CalculatePurchasePrice_UsesPurchasePrice_IfMarkdownIsSetButSpecialIsNotSet()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(1.50M)
            };

            Assert.AreEqual(7.50M, item.CalculatePurchasePrice(5));
        }

        [TestMethod]
        public void CalculatePurchasePrice_UsesOriginalPrice_IfNoMarkdownOrSpecialWasSet()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 2.00M);

            Assert.AreEqual(10.00M, item.CalculatePurchasePrice(5));
        }
    }
}

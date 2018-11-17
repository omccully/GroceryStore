using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStoreTests.Markdowns;
using GroceryStoreTests.Specials.Eaches;

namespace GroceryStoreTests
{
    [TestClass]
    public class EachesGroceryItemTests
    {
        [TestMethod]
        public void Constructor_SetsNameAndOriginalPrice()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            Assert.AreEqual("soup", item.Name);
            Assert.AreEqual(1.89M, item.OriginalPrice);
        }

        [TestMethod]
        public void PurchasePrice_GetsResultFromMarkdown()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(1.50M)
            };

            Assert.AreEqual(1.50M, item.PurchasePrice);
        }

        [TestMethod]
        public void PurchasePrice_UsesOriginalPrice_WhenNoMarkdown()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            Assert.AreEqual(1.89M, item.PurchasePrice);
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
    }
}

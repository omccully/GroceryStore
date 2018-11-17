﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStoreTests.Markdowns;
using GroceryStoreTests.Specials.Weighed;

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

        [TestMethod]
        public void CalculatePurchasePrice_UsesOriginalPrice_IfNoMarkdownOrSpecialWasSet()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.00M);

            Assert.AreEqual(5.00M, bananas.CalculatePurchasePrice(2.5M));
        }

        [TestMethod]
        public void CalculatePurchasePrice_AppliesSpecial_IfSpecialIsSet()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.00M)
            {
                Special = new WeighedGroceryItemSpecialFake(1.50M)
            };

            Assert.AreEqual(7.50M, bananas.CalculatePurchasePrice(5));
        }

        public void CalculatePurchasePrice_UsesPurchasePrice_IfMarkdownIsSetButSpecialIsNotSet()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.00M)
            {
                Markdown = new PriceMarkdownStub(1.50M)
            };

            Assert.AreEqual(7.50M, bananas.CalculatePurchasePrice(5));
        }
    }
}

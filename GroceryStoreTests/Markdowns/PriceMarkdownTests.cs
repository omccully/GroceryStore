using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStore.Cart;
using GroceryStore.Markdowns;

namespace GroceryStoreTests.Markdowns
{
    [TestClass]
    public class PriceMarkdownTests
    {
        [TestMethod]
        public void StaticPriceMarkdown_ReducesPurchasePrice()
        {
            IPriceMarkdown markdown = new PriceMarkdown(0.20M);

            Assert.AreEqual(1.69M, markdown.CalculateNewPrice(1.89M));
        }
    }
}

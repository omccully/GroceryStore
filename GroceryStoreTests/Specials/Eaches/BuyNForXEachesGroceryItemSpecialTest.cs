using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStore.Cart;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    [TestClass]
    public class BuyNForXEachesGroceryItemSpecialTest
    {
        [TestMethod]
        public void CalculateNewCost_IsFixedPrice_WhenBuyingExactAmount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);
                
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);
            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 3);

            Assert.AreEqual(5.00M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPrice_WhenNotBuyingEnough()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = 0.15M
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 2);

            Assert.AreEqual(3.48M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_IsMultipleOfFixedPrice_WhenNotBuyingMultipleOfRequiredCount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 9);

            Assert.AreEqual(15.00M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPriceForOddItems_WhenNotBuyingAmountThatIsNotAMultipleOfRequiredCount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = 0.15M
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 5);

            // the first 3 should cost 5.00M
            // the last 2 should cost (1.89-0.15) * 2 = 3.48
            // total should be 8.48M

            Assert.AreEqual(8.48M, special.CalculateNewPrice(itemOrder));
        }
    }
}

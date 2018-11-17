using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStore.Cart;
using GroceryStore.Specials.Eaches;
using GroceryStoreTests.Markdowns;

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
                
            Assert.AreEqual(5.00M, special.CalculateNewPrice(1.89M, 3));
        }

        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPrice_WhenNotBuyingEnough()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            Assert.AreEqual(3.48M, special.CalculateNewPrice(1.74M, 2));
        }

        [TestMethod]
        public void CalculateNewCost_IsMultipleOfFixedPrice_WhenNotBuyingMultipleOfRequiredCount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            Assert.AreEqual(15.00M, special.CalculateNewPrice(1.89M, 9));
        }

        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPriceForOddItems_WhenNotBuyingAmountThatIsNotAMultipleOfRequiredCount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            // the first 3 should cost 5.00M
            // the last 2 should cost (1.74M) * 2 = 3.48
            // total should be 8.48M

            Assert.AreEqual(8.48M, special.CalculateNewPrice(1.74M, 5));
        }
    }
}

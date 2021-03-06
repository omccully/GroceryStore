﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    [TestClass]
    public class BuyNForXEachesGroceryItemSpecialTest
    {
        [TestMethod]
        public void CalculateNewCost_IsFixedPrice_WhenBuyingExactAmount()
        {
            decimal expectedPrice = 5.00M;

            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, expectedPrice);
                
            Assert.AreEqual(expectedPrice, special.CalculateNewPrice(1.89M, 3));
        }

        [TestMethod]
        public void CalculateNewCost_UsesRegularPrice_WhenNotBuyingEnough()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            Assert.AreEqual(3.48M, special.CalculateNewPrice(1.74M, 2));
        }

        [TestMethod]
        public void CalculateNewCost_IsMultipleOfFixedPrice_WhenBuyingMultipleOfRequiredCount()
        {
            BuyNForXEachesGroceryItemSpecial special =
                new BuyNForXEachesGroceryItemSpecial(3, 5.00M);

            Assert.AreEqual(15.00M, special.CalculateNewPrice(1.89M, 9));
        }

        [TestMethod]
        public void CalculateNewCost_UsesRegularPriceForOddItems_WhenNotBuyingAmountThatIsNotAMultipleOfRequiredCount()
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

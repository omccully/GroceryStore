using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Specials.Weighed;

namespace GroceryStoreTests.Specials.Weighed
{
    [TestClass]
    public class BuyNGetUpToMDiscountedWeighedGroceryItemSpecialTests
    {
        [TestMethod]
        public void CalculateNewPrice_UsesRegularPrice_WhenNotBuyingEnough()
        {
            // buy up to 3.0 units, get up to 3.0 units free (100% off)
            BuyNGetUpToMDiscountedWeighedGroceryItemSpecial special =
                new BuyNGetUpToMDiscountedWeighedGroceryItemSpecial(3.0M, 100M);

            Assert.AreEqual(2.00M, special.CalculateNewPrice(1.00M, 2.0M));
        }

        [TestMethod]
        public void CalculateNewPrice_HalfTheWeightIsDiscounted_WhenBuyingTwiceTheRequiredAmount()
        {
            // buy up to 3.0 units, get up to 3.0 units 25% off
            BuyNGetUpToMDiscountedWeighedGroceryItemSpecial special =
                new BuyNGetUpToMDiscountedWeighedGroceryItemSpecial(3.0M, 25M);

            // 3.0 * 1.00 = 3.00
            // 3.0 * 0.75 = 2.25
            // = 5.25

            Assert.AreEqual(5.25M, special.CalculateNewPrice(1.00M, 6.0M));
        }

        [TestMethod]
        public void CalculateNewPrice_ExtraWeightIsDiscounted_WhenBuyingAnUnevenAmount()
        {
            // buy up to 3.0 units, get up to 3.0 units 25% off
            BuyNGetUpToMDiscountedWeighedGroceryItemSpecial special =
                new BuyNGetUpToMDiscountedWeighedGroceryItemSpecial(3.0M, 25M);

            // 3.0 * 1.00 = 3.00
            // 2.0 * 0.75 = 1.50
            // = 4.50

            Assert.AreEqual(4.50M, special.CalculateNewPrice(1.00M, 5.00M));
        }
    }
}

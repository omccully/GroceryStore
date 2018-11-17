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
    }
}

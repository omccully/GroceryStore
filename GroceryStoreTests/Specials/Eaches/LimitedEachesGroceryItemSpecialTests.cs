using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    [TestClass]
    public class LimitedEachesGroceryItemSpecialTests
    {
        [TestMethod]
        public void CalculateNewPrice_AppliesInnerSpecialOnlyToSpecifiedNumberOfItems_WhenExtraItemsAreBought()
        {
            // $1 per item
            EachesGroceryItemSpecialFake innerSpecial =
                new EachesGroceryItemSpecialFake(1.00M);

            LimitedEachesGroceryItemSpecial special =
                new LimitedEachesGroceryItemSpecial(innerSpecial, 5);

            // first 5 should be 1.00 each = 5.00
            // last 2 should be at 1.89 = 3.78
            // total should be 8.78
            Assert.AreEqual(8.78M, special.CalculateNewPrice(1.89M, 7));
        }

        [TestMethod]
        public void CalculateNewPrice_AppliesInnerSpecialToAllItems_WhenNotEnoughItemsAreBought()
        {
            // $1 per item
            EachesGroceryItemSpecialFake innerSpecial =
                new EachesGroceryItemSpecialFake(1.00M);

            LimitedEachesGroceryItemSpecial special =
                new LimitedEachesGroceryItemSpecial(innerSpecial, 5);

            Assert.AreEqual(3.00M, special.CalculateNewPrice(1.89M, 3));
        }

        [TestMethod]
        public void CalculateNewPrice_AppliesInnerSpecialToAllItems_WhenExactAmountOfItemsAreBought()
        {
            // $1 per item
            EachesGroceryItemSpecialFake innerSpecial =
                new EachesGroceryItemSpecialFake(1.00M);

            LimitedEachesGroceryItemSpecial special =
                new LimitedEachesGroceryItemSpecial(innerSpecial, 5);

            Assert.AreEqual(5.00M, special.CalculateNewPrice(1.89M, 5));
        }
    }
}

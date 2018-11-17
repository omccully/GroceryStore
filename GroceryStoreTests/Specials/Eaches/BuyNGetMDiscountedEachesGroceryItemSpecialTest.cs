using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    [TestClass]
    public class BuyNGetMDiscountedEachesGroceryItemSpecialTest
    {
        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPrice_WhenNotBuyingEnough()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            Assert.AreEqual(3.00M, special.CalculateNewPrice(1.00M, 3));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesDiscountForExtraItems_WhenBuyingMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            // buy 4 for $1 each, get one free
            Assert.AreEqual(8.00M, special.CalculateNewPrice(1.00M, 10));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesDiscountForExtraItems_WhenNotBuyingMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            // buy 4 for $1 each, get one free
            // the other 2 are $1 each

            Assert.AreEqual(6.00M, special.CalculateNewPrice(1.00M, 7));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesFreeItems_WhenBuyingMoreThanRequiredAmountButNotMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(3, 3, 100M);

            // the special is for buy 3, get 3 free
            // but we want the customer to still be able to get 2 free items
            // when they have 5 items in their cart

            Assert.AreEqual(3.00M, special.CalculateNewPrice(1.00M, 5));
        }
    }
}

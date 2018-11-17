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
    public class BuyNGetMDiscountedEachesGroceryItemSpecialTest
    {
        [TestMethod]
        public void CalculateNewCost_UsesMarkdownPrice_WhenNotBuyingEnough()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                // a PriceMarkdownStub is used here to make sure
                // that the special is using the EachesGroceryItem's 
                // PurchasePrice instead of the OriginalPrice
                // for the non-discounted items

                // the PriceMarkdownStub provides a constant PurchasePrice
                // I don't think it's worth it to make a fake 
                // EachesGroceryItem class just to avoid retesting 
                // the small amount of PurchasePrice accessor logic
                Markdown = new PriceMarkdownStub(1.00M)
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 3);

            Assert.AreEqual(3.00M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesDiscountForExtraItems_WhenBuyingMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(1.00M)
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 10);

            // buy 4 for $1 each, get one free
            Assert.AreEqual(8.00M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesDiscountForExtraItems_WhenNotBuyingMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(4, 1, 100M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(1.00M)
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 7);

            // buy 4 for $1 each, get one free
            // the other 2 are $1 each

            Assert.AreEqual(6.00M, special.CalculateNewPrice(itemOrder));
        }

        [TestMethod]
        public void CalculateNewCost_ProvidesFreeItems_WhenBuyingMoreThanRequiredAmountButNotMultipleOfSaleAmount()
        {
            BuyNGetMDiscountedEachesGroceryItemSpecial special =
                new BuyNGetMDiscountedEachesGroceryItemSpecial(3, 3, 100M);

            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Markdown = new PriceMarkdownStub(1.00M)
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 5);

            // the special is for buy 3, get 3 free
            // but we want the customer to still be able to get 2 free items
            // when they have 5 items in their cart

            Assert.AreEqual(3.00M, special.CalculateNewPrice(itemOrder));
        }
    }
}

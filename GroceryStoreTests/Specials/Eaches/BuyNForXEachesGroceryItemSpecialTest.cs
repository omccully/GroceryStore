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
                // a PriceMarkdownStub is used here to make sure
                // that the special is using the EachesGroceryItem's 
                // PurchasePrice instead of the OriginalPrice
                // for the non-discounted items

                // the PriceMarkdownStub provides a constant PurchasePrice
                // I don't think it's worth it to make a fake 
                // EachesGroceryItem class just to avoid retesting 
                // the small amount of PurchasePrice accessor logic
                Markdown = new PriceMarkdownStub(1.74M)
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
                Markdown = new PriceMarkdownStub(1.74M)
            };

            EachesGroceryItemOrder itemOrder =
                new EachesGroceryItemOrder(item, 5);

            // the first 3 should cost 5.00M
            // the last 2 should cost (1.74M) * 2 = 3.48
            // total should be 8.48M

            Assert.AreEqual(8.48M, special.CalculateNewPrice(itemOrder));
        }
    }
}

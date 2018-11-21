using System;
using GroceryStore.Cart;
using GroceryStore.Specials.Eaches;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GroceryStore.Specials.Weighed;

namespace GroceryStoreTests.Integration
{
    [TestClass]
    public class CheckoutCartIntegrationTests
    {
        [TestMethod]
        public void RemovingEachesOrder_InvalidatesSpecial()
        {
            decimal priceForThree = 5.00M;
            EachesGroceryItem soup = new EachesGroceryItem("soup", 2.00M)
            {
                Special = new BuyNForXEachesGroceryItemSpecial(3, priceForThree)
            };

            CheckoutCart cart = new CheckoutCart();
            for(int i = 0; i < 3; i++)
            {
                cart.Orders.Add(new EachesGroceryItemOrder(soup, 1));
            }

            Assert.AreEqual(priceForThree, cart.TotalPrice);
            cart.Orders.Remove(cart.Orders.Last());
            Assert.AreEqual(4.00M, cart.TotalPrice);
        }

        [TestMethod]
        public void RemovingWeighedOrder_InvalidatesSpecial()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.00M)
            {
                Special = new BuyNGetUpToMDiscountedWeighedGroceryItemSpecial(3.00M, 50M)
            };

            CheckoutCart cart = new CheckoutCart();
            for (int i = 0; i < 4; i++)
            {
                cart.Orders.Add(new WeighedGroceryItemOrder(bananas, 1));
            }

            // first 3 are full price ($2 per unit)
            // last is 50% off, $1
            Assert.AreEqual(7.00M, cart.TotalPrice);
            cart.Orders.Remove(cart.Orders.Last());
            Assert.AreEqual(6.00M, cart.TotalPrice);
        }
    }
}

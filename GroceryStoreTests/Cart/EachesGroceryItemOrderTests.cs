using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStore.Cart;
using GroceryStoreTests.Specials.Eaches;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class EachesGroceryItemOrderTests
    {
        [TestMethod]
        public void Constructor_SetsItemAndCount()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M);
            EachesGroceryItemOrder order = new EachesGroceryItemOrder(item, 3);

            Assert.AreEqual(item, order.Item);
            Assert.AreEqual(3, order.Count);
        }

        [TestMethod]
        public void Price_CalculatesUsingPurchasePriceAndSpecials()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 1.89M)
            {
                Special = new EachesGroceryItemSpecialFake(1.00M)
            };
            EachesGroceryItemOrder order = new EachesGroceryItemOrder(item, 3);

            Assert.AreEqual(3.00M, order.Price);

        }
    }
}

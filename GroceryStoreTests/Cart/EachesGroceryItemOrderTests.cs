using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStore.Stock;
using GroceryStore.Cart;

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
    }
}

﻿using System;
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
        public void Price_CalculatesUsingItemMethod()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 2.00M);

            EachesGroceryItemOrder order = new EachesGroceryItemOrder(item, 3);

            Assert.AreEqual(6.00M, order.Price);
        }

        [TestMethod]
        public void Item_IsImplementedForGroceryItemInterface()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 2.00M);

            EachesGroceryItemOrder order = new EachesGroceryItemOrder(item, 3);

            IGroceryItemOrder abstractOrder = order;

            Assert.AreEqual(item, abstractOrder.Item);
        }

        [TestMethod]
        public void Combine_ReturnsNewEachesGroceryItemWithCombinedCount()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 2.00M);
            EachesGroceryItemOrder a = new EachesGroceryItemOrder(item, 3);
            EachesGroceryItemOrder b = new EachesGroceryItemOrder(item, 6);

            EachesGroceryItemOrder result = a.Combine(b);

            Assert.AreEqual(9, result.Count);
        }

        [TestMethod]
        public void AbstractCombine_ReturnsNewEachesGroceryItemWithCombinedCount()
        {
            EachesGroceryItem item = new EachesGroceryItem("soup", 2.00M);
            IGroceryItemOrder a = new EachesGroceryItemOrder(item, 3);
            IGroceryItemOrder b = new EachesGroceryItemOrder(item, 6);

            EachesGroceryItemOrder result = (EachesGroceryItemOrder)a.Combine(b);

            Assert.AreEqual(9, result.Count);
        }
    }
}

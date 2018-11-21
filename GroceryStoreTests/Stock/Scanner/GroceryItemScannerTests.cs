using System;
using System.Linq;
using System.Collections.Generic;
using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using GroceryStore.Stock.Scanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Stock.Scanner
{
    [TestClass]
    public class GroceryItemScannerTests
    {
        [TestMethod]
        public void Constructor_InitializesItems_WhenPassedGroceryItemEnumerable()
        {
            IEnumerable<IGroceryItem> items = new List<IGroceryItem>()
            {
                new WeighedGroceryItem("bananas", 2.38M)
            };

            GroceryItemScanner scanner = new GroceryItemScanner(items, null);

            CollectionAssert.AreEquivalent(items.ToList(), scanner.Items.ToList());
        }

        [TestMethod]
        public void Items_CanAddEachesGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner(null);

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new EachesGroceryItem("soup", 1.89M));
            Assert.AreEqual(1, scanner.Items.Count);
        }

        [TestMethod]
        public void Items_CanAddWeighedGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner(null);

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));
            Assert.AreEqual(1, scanner.Items.Count);
        }

        [TestMethod]
        public void Scan_ReturnsCorrespondingItem_IfExists()
        {
            GroceryItemScanner scanner = new GroceryItemScanner(null);
            IGroceryItem expected = new WeighedGroceryItem("bananas", 2.38M);
            scanner.Items.Add(expected);

            IGroceryItem result = scanner.Scan("bananas");
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void Scan_ThrowsException_IfDoesntExist()
        {
            GroceryItemScanner scanner = new GroceryItemScanner(null);

            Assert.ThrowsException<GroceryItemNotFoundException>(() => 
                scanner.Scan("bananas"));
        }

        [TestMethod]
        public void Scan_ThrowsException_IfParamterMatchesMoreThanOne()
        {
            GroceryItemScanner scanner = new GroceryItemScanner(null);
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));
            scanner.Items.Add(new EachesGroceryItem("bananas", 1.00M));

            Assert.ThrowsException<DuplicateGroceryItemException>(() =>
                scanner.Scan("bananas"));
        }

        [TestMethod]
        public void Scan_ThrowsExceptionContainingTheDuplicateItems_IfParamterMatchesMoreThanOne()
        {
            List<IGroceryItem> duplicateItems = new List<IGroceryItem>()
            {
                new WeighedGroceryItem("bananas", 2.38M),
                new EachesGroceryItem("bananas", 1.00M)
            };

            GroceryItemScanner scanner = new GroceryItemScanner(null);

            scanner.Items.Add(new EachesGroceryItem("soup", 1.89M));
            foreach(IGroceryItem item in duplicateItems)
            {
                scanner.Items.Add(item);
            }
           
            try
            {
                scanner.Scan("bananas");
                Assert.Fail();
            }
            catch(DuplicateGroceryItemException e)
            {
                CollectionAssert.AreEquivalent(duplicateItems, e.Duplicates.ToList());
            }
        }

        [TestMethod]
        public void CreateOrder_CreatesOrderFromOrderFactory()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.38M);
            

            IGroceryItemOrder bananasOrder =
                new WeighedGroceryItemOrder(bananas, 0.0M);
            Mock<IGroceryItemOrderFactory> orderFactoryMock =
                new Mock<IGroceryItemOrderFactory>();
            orderFactoryMock.Setup(of => of.CreateOrder(bananas))
                .Returns(bananasOrder);

            GroceryItemScanner scanner = new GroceryItemScanner(orderFactoryMock.Object);
            scanner.Items.Add(bananas);

            IGroceryItemOrder orderResult = scanner.CreateOrder("bananas");

            Assert.AreEqual(bananasOrder, orderResult);
        }
    }
}

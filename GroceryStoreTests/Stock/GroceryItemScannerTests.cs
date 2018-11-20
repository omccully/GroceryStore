using System;
using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Stock
{
    [TestClass]
    public class GroceryItemScannerTests
    {
        [TestMethod]
        public void Items_CanAddEachesGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new EachesGroceryItem("soup", 1.89M));
            Assert.AreEqual(1, scanner.Items.Count);
        }

        [TestMethod]
        public void Items_CanAddWeighedGroceryItem()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();

            Assert.AreEqual(0, scanner.Items.Count);
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));
            Assert.AreEqual(1, scanner.Items.Count);
        }

        [TestMethod]
        public void Scan_ReturnsCorrespondingItem_IfExists()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();
            IGroceryItem expected = new WeighedGroceryItem("bananas", 2.38M);
            scanner.Items.Add(expected);

            IGroceryItem result = scanner.Scan("bananas");
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void Scan_ThrowsException_IfDoesntExist()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();

            Assert.ThrowsException<GroceryItemNotFoundException>(() => 
                scanner.Scan("bananas"));
        }

        [TestMethod]
        public void Scan_ThrowsException_IfParamterMatchesMoreThanOne()
        {
            GroceryItemScanner scanner = new GroceryItemScanner();
            scanner.Items.Add(new WeighedGroceryItem("bananas", 2.38M));
            scanner.Items.Add(new EachesGroceryItem("bananas", 1.00M));

            Assert.ThrowsException<DuplicateGroceryItemException>(() =>
                scanner.Scan("bananas"));
        }

        [TestMethod]
        public void CreateOrder_CreatesOrderFromOrderFactory()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.38M);
            GroceryItemScanner scanner = new GroceryItemScanner();
            scanner.Items.Add(bananas);

            IGroceryItemOrder bananasOrder =
                new WeighedGroceryItemOrder(bananas, 0.0M);
            Mock<IGroceryItemOrderFactory> orderFactoryMock =
                new Mock<IGroceryItemOrderFactory>();
            orderFactoryMock.Setup(of => of.CreateOrder(bananas))
                .Returns(bananasOrder);

            scanner.OrderFactory = orderFactoryMock.Object;

            IGroceryItemOrder orderResult = scanner.CreateOrder("bananas");

            Assert.AreEqual(bananasOrder, orderResult);
        }
    }
}

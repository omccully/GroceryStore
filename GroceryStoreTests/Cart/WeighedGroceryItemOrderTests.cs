using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class WeighedGroceryItemOrderTests
    {
        [TestMethod]
        public void Constructor_SetsItemAndWeight()
        {
            decimal expectedWeight = 1.6M;
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(bananas, expectedWeight);

            Assert.AreEqual(bananas, order.Item);
            Assert.AreEqual(expectedWeight, order.Weight);
        }

        [TestMethod]
        public void Price_CalculatesUsingItemMethod()
        {
            Mock<IGroceryItem<decimal>> itemMock = new Mock<IGroceryItem<decimal>>();
            itemMock.Setup((item) => item.CalculatePurchasePrice(It.IsAny<decimal>()))
                .Returns((decimal weight) => weight * 2.0M);

            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(itemMock.Object, 1.5M);

            // 1.5M * 2.0M = 3.0M
            Assert.AreEqual(3.00M, order.Price);
        }

        [TestMethod]
        public void Combine_ReturnsNewWeighedGroceryItemWithCombinedWeight()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder a = new WeighedGroceryItemOrder(bananas, 1.2M);
            WeighedGroceryItemOrder b = new WeighedGroceryItemOrder(bananas, 5.0M);

            WeighedGroceryItemOrder result = a.Combine(b);

            Assert.AreEqual(6.2M, result.Weight);
        }

        [TestMethod]
        public void AbstractCombine_ReturnsNewWeighedGroceryItemWithCombinedWeight()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            IGroceryItemOrder a = new WeighedGroceryItemOrder(bananas, 1.2M);
            IGroceryItemOrder b = new WeighedGroceryItemOrder(bananas, 5.0M);

            WeighedGroceryItemOrder result = (WeighedGroceryItemOrder)a.Combine(b);

            Assert.AreEqual(6.2M, result.Weight);
        }

        [TestMethod]
        public void Combine_ThrowsException_IfItemsAreDifferent()
        {
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItem potatoes = new WeighedGroceryItem("potatoes", 1.38M);

            WeighedGroceryItemOrder a = new WeighedGroceryItemOrder(bananas, 1.2M);
            WeighedGroceryItemOrder b = new WeighedGroceryItemOrder(potatoes, 5.0M);

            Assert.ThrowsException<DifferingItemsException>(() => a.Combine(b));
        }
    }
}

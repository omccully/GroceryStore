using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStoreTests.Cart
{
    [TestClass]
    public class WeighedGroceryItemOrderTests
    {
        [TestMethod]
        public void Constructor_SetsItemAndWeight()
        {
            decimal weight = 1.6M;
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(bananas, weight);

            Assert.AreEqual(bananas, order.Item);
            Assert.AreEqual(weight, order.Weight);
        }

        [TestMethod]
        public void Price_CalculatesUsingItemMethod()
        {
            decimal weight = 1.6M;
            WeighedGroceryItem bananas = new WeighedGroceryItem("bananas", 2.30M);
            WeighedGroceryItemOrder order = new WeighedGroceryItemOrder(bananas, weight);

            // 1.6 * 2.3 = 3.68

            Assert.AreEqual(3.68M, order.Price);
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

            Assert.ThrowsException<InvalidGroceryItemTypeException>(() => a.Combine(b));
        }
    }
}

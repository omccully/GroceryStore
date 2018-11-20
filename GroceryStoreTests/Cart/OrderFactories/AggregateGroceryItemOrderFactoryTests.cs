using System;
using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStoreTests.Cart.OrderFactories
{
    [TestClass]
    public class AggregateGroceryItemOrderFactoryTests
    {
        [TestMethod]
        public void CreateOrder_UsesSecondFactory_WhenFirstFactoryThrowsException()
        {
            IGroceryItemOrder expectedOrder = new Mock<IGroceryItemOrder>().Object;

            Mock<IGroceryItemOrderFactory> firstFactory = new Mock<IGroceryItemOrderFactory>();
            firstFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
                .Throws<InvalidGroceryItemTypeException>();

            Mock<IGroceryItemOrderFactory> secondFactory = new Mock<IGroceryItemOrderFactory>();
            secondFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
                .Returns(expectedOrder);

            AggregateGroceryItemOrderFactory factory =
                new AggregateGroceryItemOrderFactory(firstFactory.Object, 
                secondFactory.Object);

            Assert.AreEqual(expectedOrder, factory.CreateOrder(null));
        }

        [TestMethod]
        public void CreateOrder_UsesFirstFactory_WhenFirstFactoryReturnsResult()
        {
            IGroceryItemOrder expectedOrder = new Mock<IGroceryItemOrder>().Object;
            IGroceryItemOrder unexpectedOrder = new Mock<IGroceryItemOrder>().Object;

            Mock<IGroceryItemOrderFactory> firstFactory = new Mock<IGroceryItemOrderFactory>();
            firstFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
                .Returns(expectedOrder);

            Mock<IGroceryItemOrderFactory> secondFactory = new Mock<IGroceryItemOrderFactory>();
            secondFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
                .Returns(unexpectedOrder);

            AggregateGroceryItemOrderFactory factory =
                new AggregateGroceryItemOrderFactory(firstFactory.Object,
                secondFactory.Object);

            Assert.AreEqual(expectedOrder, factory.CreateOrder(null));
            Assert.AreNotEqual(unexpectedOrder, factory.CreateOrder(null));
        }

        [TestMethod]
        public void CreateOrder_ThrowsException_WhenAllFactoriesThrowException()
        {
            Mock<IGroceryItemOrderFactory> firstFactory = new Mock<IGroceryItemOrderFactory>();
            firstFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
               .Throws<InvalidGroceryItemTypeException>();

            Mock<IGroceryItemOrderFactory> secondFactory = new Mock<IGroceryItemOrderFactory>();
            secondFactory.Setup(f => f.CreateOrder(It.IsAny<IGroceryItem>()))
                .Throws<InvalidGroceryItemTypeException>();

            AggregateGroceryItemOrderFactory factory =
                new AggregateGroceryItemOrderFactory(firstFactory.Object,
                secondFactory.Object);

            Assert.ThrowsException<InvalidGroceryItemTypeException>(
                () => factory.CreateOrder(null));
        }
    }
}

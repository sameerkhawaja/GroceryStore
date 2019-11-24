using GroceryStore.Shopping;
using GroceryStore.Shopping.Interfaces;
using GroceryStore.Shopping.StringConstants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ShoppingCart_Tests
    {
        Mock<IStoreInventorySystem> mockStoreInventorySystem;
        [TestInitialize]
        public void TestInit()
        {
            mockStoreInventorySystem = new Mock<IStoreInventorySystem>();
        }

        [TestMethod]
        public void GetPrice_When_ItemIsInStoreInventory_ShouldReturnPrice()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(It.IsAny<decimal>());
            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var price = shoppingCart.GetPrice("some item");

            Assert.IsInstanceOfType(price, typeof(decimal));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidItemException), "An invalid item was scanned.")]
        public void GetPrice_When_ItemIsNull_ShouldReturnPrice()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice(It.IsAny<string>())).Throws<InvalidItemException>();
            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var price = shoppingCart.GetPrice(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidItemException), "An invalid item was scanned.")]
        public void GetPrice_When_ItemIsNotInStoreInventory_ShouldThrowException()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice(It.IsAny<string>())).Throws<InvalidItemException>();
            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var price = shoppingCart.GetPrice("some invalid item");
        }

        [TestMethod]
        public void Checkout_When_ItemsIsEmpty_ShouldReturnZero()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(It.IsAny<decimal>());
            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var items = new Dictionary<string, int>();
            var subtotal = shoppingCart.Checkout(items);

            Assert.AreEqual(0m, subtotal);
        }

        [TestMethod]
        public void Checkout_When_ItemsIsNull_ShouldReturnZero()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(It.IsAny<decimal>());
            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var price = shoppingCart.Checkout(null);
            Assert.AreEqual(0m, price);
        }

        [TestMethod]
        public void Checkout_When_ThereIsOneItem_ShouldReturnPriceOfItem()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice("Apple")).Returns(1.50m);

            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var items = new Dictionary<string, int>
            {
                { StoreInventoryConstants.Apple, 1 }
            };


            var price = shoppingCart.Checkout(items);
            Assert.AreEqual(1.50m, price);
        }

        [TestMethod]
        public void Checkout_When_ThereAreManyItems_ShouldReturnSubtotal()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice("Apple")).Returns(1.50m);
            mockStoreInventorySystem.Setup(x => x.GetPrice("Orange")).Returns(2.22m);

            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var items = new Dictionary<string, int>
            {
                { StoreInventoryConstants.Apple, 3 },
                { StoreInventoryConstants.Orange, 1 },
            };

            var price = shoppingCart.Checkout(items);

            mockStoreInventorySystem.Verify(x => x.GetPrice(It.IsAny<string>()), Times.Exactly(items.Count));
            Assert.AreEqual(5.22m, price);
        }

        [TestMethod]
        public void Checkout_When_ThereAreManyItems_ShouldApplyOffers()
        {
            mockStoreInventorySystem.Setup(x => x.GetPrice("Apple")).Returns(1m);
            mockStoreInventorySystem.Setup(x => x.GetPrice("Orange")).Returns(1m);

            var shoppingCart = new ShoppingCart(mockStoreInventorySystem.Object);

            var items = new Dictionary<string, int>
            {
                { StoreInventoryConstants.Apple, 9 },
                { StoreInventoryConstants.Orange, 10 },
            };

            var price = shoppingCart.Checkout(items);

            mockStoreInventorySystem.Verify(x => x.GetPrice(It.IsAny<string>()), Times.Exactly(items.Count));
            Assert.AreEqual(12m, price);
        }

        
    }
}

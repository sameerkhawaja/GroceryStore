using GroceryStore.Shopping;
using GroceryStore.Shopping.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class Offer_Tests
    {
        Mock<IStoreInventorySystem> mockStoreInventorySystem;

        [TestInitialize]
        public void TestInit()
        {
            mockStoreInventorySystem = new Mock<IStoreInventorySystem>();
        }

        [TestMethod]
        public void XForThePriceOfY_When_ThereIsOneItem_ShouldNotApplyOffer()
        {
            var offer = new Offer(mockStoreInventorySystem.Object);

            int numberOfItems = 1;
            decimal cost = 1;

            var price = offer.XForThePriceOfYCalculator(3, 2, cost, numberOfItems);
            Assert.AreEqual(1, price);
        }
        [TestMethod]
        public void XForThePriceOfY_When_ThereIsMultipleItems_AndNotEnoughForOffer_ShouldNotApplyOffer()
        {
            var offer = new Offer(mockStoreInventorySystem.Object);

            int numberOfItems = 2;
            decimal cost = 1;

            var price = offer.XForThePriceOfYCalculator(3, 1, cost, numberOfItems);
            Assert.AreEqual(2, price);
        }

        [TestMethod]
        public void XForThePriceOfY_When_ThereIsMultipleItems_AndEqualToOffer_ShouldApplyOffer()
        {
            var offer = new Offer(mockStoreInventorySystem.Object);

            int numberOfItems = 3;
            decimal cost = 1;

            var price = offer.XForThePriceOfYCalculator(3, 1, cost, numberOfItems);
            Assert.AreEqual(1, price);
        }

        [TestMethod]
        public void XForThePriceOfY_When_ThereIsMultipleItems_AndOfferIsAppliedMultipleTimes_ShouldApplyOfferMultipleTimes()
        {
            var offer = new Offer(mockStoreInventorySystem.Object);

            int numberOfItems = 11;
            decimal cost = 1;

            var price = offer.XForThePriceOfYCalculator(5, 3, cost, numberOfItems);
            Assert.AreEqual(7, price);
        }
    }
}

using GroceryStore.Shopping.Interfaces;
using GroceryStore.Shopping.StringConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Shopping
{
    public class Offer: IOffer
    {
        readonly IStoreInventorySystem storeInventorySystem;

        public Offer(IStoreInventorySystem storeInventorySystem)
        {
            this.storeInventorySystem = storeInventorySystem;
        }

        public Tuple<int, int> GetPricingStrategy(string item)
        {
            var pricingStrategies = BuildPricingStrategies();

            if (pricingStrategies.TryGetValue(item, out Tuple<int, int> offer))
            {
                return offer;
            }
            else
            {
                return new Tuple<int, int>(1, 1);
            }
        }

        public decimal XForThePriceOfYCalculator(int x, int y, decimal price, int numberOfItems)
        {
            var numberOfFullPricedItems = numberOfItems % x;
            var numberOfTimesOfferApplied = numberOfItems / x;

            decimal subtotal = (price * numberOfFullPricedItems) + (price * numberOfTimesOfferApplied * y);

            return subtotal;
        }

        private Dictionary<string, Tuple<int, int>> BuildPricingStrategies()
        {
            var pricingStrategy = new Dictionary<string, Tuple<int, int>>
            {
                { StoreInventoryConstants.Apple, new Tuple<int, int>(2, 1) },
                { StoreInventoryConstants.Orange, new Tuple<int, int>(3, 2) }
            };

            return pricingStrategy;
        }
    }
}

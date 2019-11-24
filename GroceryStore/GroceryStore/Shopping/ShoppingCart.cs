using GroceryStore.Shopping.Interfaces;
using GroceryStore.Shopping.StringConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Shopping
{
    public class ShoppingCart : IShoppingCart
    {
        readonly IStoreInventorySystem storeInventorySystem;

        public ShoppingCart(IStoreInventorySystem storeInventorySystem)
        {
            this.storeInventorySystem = storeInventorySystem;
        }

        public decimal GetPrice(string item)
        {
            return storeInventorySystem.GetPrice(item);
        }

        public decimal Checkout(Dictionary<string, int> items)
        {
            if (items == null)
            {
                return 0;
            }

            var subtotal = 0m;

            foreach (KeyValuePair<string, int> item in items)
            {
                subtotal += CalculatePricing(item);
            }
            return subtotal;
        }

        private decimal CalculatePricing(KeyValuePair<string, int> item)
        {
            var price = GetPrice(item.Key);

            if (item.Key == StoreInventoryConstants.Apple)
            {
                var numberOfFullPricedItems = item.Value % 2;
                var numberOfTimesOfferApplied = item.Value / 2;

                decimal subtotal = (price * numberOfFullPricedItems) + (price * numberOfTimesOfferApplied);

                return subtotal;
            }
            else if (item.Key == StoreInventoryConstants.Orange)
            {
                var numberOfFullPricedItems = item.Value % 3;
                var numberOfTimesOfferApplied = item.Value / 3;

                decimal subtotal = (price * numberOfFullPricedItems) + (price * numberOfTimesOfferApplied * 2);

                return subtotal;
            }
            else
            {
                return price * item.Value;
            }
        }
    }
}

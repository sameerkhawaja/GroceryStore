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

        public decimal CalculatePricing(KeyValuePair<string, int> item)
        {
            var price = storeInventorySystem.GetPrice(item.Key);
            var offer = new Offer(storeInventorySystem);

            var pricingStrategy = offer.GetPricingStrategy(item.Key);

            var x = pricingStrategy.Item1;
            var y = pricingStrategy.Item2;

            return offer.XForThePriceOfYCalculator(x, y, price, item.Value);
        }

    }
}

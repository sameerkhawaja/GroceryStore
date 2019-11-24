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
                for (int i = 0; i < item.Value; i++)
                {
                    subtotal += GetPrice(item.Key);
                }
            }
            return subtotal;
        }


    }
}

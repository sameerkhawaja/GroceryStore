using GroceryStore.Shopping;
using GroceryStore.Shopping.StringConstants;
using System;
using System.Collections.Generic;

namespace GroceryStore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var storeInventorySystem = new StoreInventorySystem();
            var shoppingCart = new ShoppingCart(storeInventorySystem);

            var items = new List<string>
            {
                StoreInventoryConstants.Apple,
                StoreInventoryConstants.Apple,
                StoreInventoryConstants.Orange,
                StoreInventoryConstants.Apple
            };

            var subtotal = shoppingCart.Checkout(items);
            Console.WriteLine($"Your subtotal is \u00A3{subtotal}");

        }
    }
}
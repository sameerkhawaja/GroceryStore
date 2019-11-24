using GroceryStore.Shopping;
using GroceryStore.Shopping.StringConstants;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var itemsGrouped = items.GroupBy(x => x).
                Select(x => new { Items = x.Key, Count = x.Count() });
            var itemsDictionary = itemsGrouped.ToDictionary(x => x.Items, x => x.Count);

            var subtotal = shoppingCart.Checkout(itemsDictionary);
            Console.WriteLine($"Your subtotal is \u00A3{subtotal}");
        }
    }
}
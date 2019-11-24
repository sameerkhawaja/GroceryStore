using GroceryStore.Shopping.Interfaces;
using GroceryStore.Shopping.StringConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Shopping
{
    public class StoreInventorySystem : IStoreInventorySystem
    {
        public decimal GetPrice(string item)
        {
            return item switch
            {
                StoreInventoryConstants.Apple => 0.60m,

                StoreInventoryConstants.Orange => 0.25m,

                _ => throw new InvalidItemException("Invalid item in shopping cart: {item}"),
            };
        }
    }
}

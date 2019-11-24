using System.Collections.Generic;

namespace GroceryStore.Shopping.Interfaces
{
    public interface IShoppingCart
    {
        decimal GetPrice(string item);
        decimal Checkout(Dictionary<string, int> items);
    }
}
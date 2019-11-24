using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Shopping.Interfaces
{
    public interface IStoreInventorySystem
    {
        decimal GetPrice(string item);
    }
}

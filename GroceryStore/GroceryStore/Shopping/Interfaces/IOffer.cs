using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Shopping.Interfaces
{
    public interface IOffer
    {
        public Tuple<int, int> GetPricingStrategy(string item);
        public decimal XForThePriceOfYCalculator(int x, int y, decimal price, int numberOfItems);
    }
}

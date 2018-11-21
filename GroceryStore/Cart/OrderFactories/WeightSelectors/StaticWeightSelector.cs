using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories.WeightSelectors
{
    class StaticWeightSelector : IWeightSelector
    {
        decimal StaticWeight;

        public StaticWeightSelector(decimal staticWeight)
        {
            this.StaticWeight = staticWeight;
        }

        public decimal SelectWeight(IGroceryItem<decimal> item)
        {
            return StaticWeight;
        }
    }
}

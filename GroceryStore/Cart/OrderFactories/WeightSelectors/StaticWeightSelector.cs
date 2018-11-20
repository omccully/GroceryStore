using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart.OrderFactories.WeightSelectors
{
    class StaticWeightSelector : IWeightSelector
    {
        decimal StaticWeight;

        public StaticWeightSelector(decimal staticWeight)
        {
            this.StaticWeight = staticWeight;
        }

        public decimal SelectWeight()
        {
            return StaticWeight;
        }
    }
}

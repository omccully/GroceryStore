using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart.OrderFactories.WeightSelectors;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories
{
    public class WeighedGroceryItemOrderFactory : IGroceryItemOrderFactory
    {
        const decimal DefaultWeight = 0.0M;
        IWeightSelector WeightSelector;

        public WeighedGroceryItemOrderFactory()
            : this(DefaultWeight)
        {
            
        }

        public WeighedGroceryItemOrderFactory(decimal defaultWeighedItemWeight)
        {
            this.WeightSelector = new StaticWeightSelector(defaultWeighedItemWeight);
        }

        public WeighedGroceryItemOrderFactory(IWeightSelector weightSelector)
        {
            this.WeightSelector = weightSelector;
        }

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            WeighedGroceryItem weighedItem = item as WeighedGroceryItem;

            if (weighedItem == null)
                throw new InvalidGroceryItemTypeException();

            decimal weight = WeightSelector.SelectWeight();

            return new WeighedGroceryItemOrder(weighedItem, weight);
        }
    }
}

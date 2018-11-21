using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart.OrderFactories.CountSelectors;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories
{
    public class EachesGroceryItemOrderFactory : IGroceryItemOrderFactory
    {
        const int DefaultCount = 1;

        ICountSelector CountSelector;

        public EachesGroceryItemOrderFactory()
            : this(DefaultCount)
        {
        }

        public EachesGroceryItemOrderFactory(int defaultEachesItemCount)
        {
            this.CountSelector = new StaticCountSelector(defaultEachesItemCount);
        }

        public EachesGroceryItemOrderFactory(ICountSelector countSelector)
        {
            this.CountSelector = countSelector;
        }

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            EachesGroceryItem eachesItem = item as EachesGroceryItem;

            if (eachesItem == null)
                throw new InvalidGroceryItemTypeException();

            int count = CountSelector.SelectCount();

            return new EachesGroceryItemOrder(eachesItem, count);
        }
    }
}

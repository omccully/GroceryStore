using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories
{
    public class EachesGroceryItemOrderFactory : IGroceryItemOrderFactory
    {
        int DefaultEachesItemCount;
        ICountSelector CountSelector;

        public EachesGroceryItemOrderFactory()
        {
            DefaultEachesItemCount = 1;
        }

        public EachesGroceryItemOrderFactory(int defaultEachesItemCount)
        {
            this.DefaultEachesItemCount = defaultEachesItemCount;
        }

        public EachesGroceryItemOrderFactory(ICountSelector countSelector)
        {
            this.CountSelector = countSelector;
        }

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            IEachesGroceryItem eachesItem = item as EachesGroceryItem;

            if (eachesItem == null)
                throw new InvalidGroceryItemTypeException();

            int count = CountSelector == null ? DefaultEachesItemCount :
                CountSelector.SelectCount();

            return new EachesGroceryItemOrder(eachesItem, count);
        }
    }
}

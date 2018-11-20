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
        const int DefaultEachesItemCount = 1;

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            IEachesGroceryItem eachesItem = item as EachesGroceryItem;

            if (eachesItem == null)
                throw new InvalidGroceryItemTypeException();

            return new EachesGroceryItemOrder(eachesItem, DefaultEachesItemCount);
        }
    }
}

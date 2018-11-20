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

            return new EachesGroceryItemOrder(eachesItem, DefaultEachesItemCount);
        }
    }
}

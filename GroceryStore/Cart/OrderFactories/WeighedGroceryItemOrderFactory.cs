using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories
{
    public class WeighedGroceryItemOrderFactory : IGroceryItemOrderFactory
    {
        decimal DefaultWeighedItemWeight;

        public WeighedGroceryItemOrderFactory()
        {
            this.DefaultWeighedItemWeight = 0.0M;
        }

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            IWeighedGroceryItem weighedItem = item as IWeighedGroceryItem;

            if (weighedItem == null)
                throw new InvalidGroceryItemTypeException();

            return new WeighedGroceryItemOrder(weighedItem, DefaultWeighedItemWeight);
        }
    }
}

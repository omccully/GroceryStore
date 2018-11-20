using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories
{
    public class AggregateGroceryItemOrderFactory : IGroceryItemOrderFactory
    {
        List<IGroceryItemOrderFactory> Factories;

        public AggregateGroceryItemOrderFactory(
            params IGroceryItemOrderFactory[] factories)
            : this((IEnumerable<IGroceryItemOrderFactory>)factories)
        {
            
        }

        public AggregateGroceryItemOrderFactory(
            IEnumerable<IGroceryItemOrderFactory> factories)
        {
            Factories = new List<IGroceryItemOrderFactory>(factories);
        }

        public IGroceryItemOrder CreateOrder(IGroceryItem item)
        {
            foreach(IGroceryItemOrderFactory factory in Factories)
            {
                try
                {
                    return factory.CreateOrder(item);
                }
                catch(InvalidGroceryItemTypeException)
                {

                }
            }

            throw new InvalidGroceryItemTypeException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart.OrderFactories.CountSelectors
{
    class StaticCountSelector : ICountSelector
    {
        int StaticCount;

        public StaticCountSelector(int staticCount)
        {
            this.StaticCount = staticCount;
        }

        public int SelectCount(IGroceryItem<int> item)
        {
            return StaticCount;
        }
    }
}

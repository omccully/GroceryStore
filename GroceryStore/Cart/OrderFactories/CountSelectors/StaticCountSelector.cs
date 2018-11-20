using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart.OrderFactories.CountSelectors
{
    class StaticCountSelector : ICountSelector
    {
        int StaticCount;

        public StaticCountSelector(int staticCount)
        {
            this.StaticCount = staticCount;
        }

        public int SelectCount()
        {
            return StaticCount;
        }
    }
}

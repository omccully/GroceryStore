using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class EachesGroceryItemOrder
    {
        public EachesGroceryItem Item { get; private set; }
        public int Count { get; set; }

        public EachesGroceryItemOrder(EachesGroceryItem item, int count = 1)
        {
            this.Item = item;
            this.Count = count;
        }
    }
}

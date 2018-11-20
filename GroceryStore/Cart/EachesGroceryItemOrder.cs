using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart
{
    public class EachesGroceryItemOrder : IGroceryItemOrder
    {
        public IEachesGroceryItem Item { get; private set; }
        public int Count { get; set; }

        public decimal Price
        {
            get
            {
                return Item.CalculatePurchasePrice(Count);
            }
        }

        IGroceryItem IGroceryItemOrder.Item => Item;

        public EachesGroceryItemOrder(IEachesGroceryItem item, int count = 1)
        {
            this.Item = item;
            this.Count = count;
        }
    }
}

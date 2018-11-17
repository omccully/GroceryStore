using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart
{
    public class EachesGroceryItemOrder
    {
        public EachesGroceryItem Item { get; private set; }
        public int Count { get; set; }

        public decimal Price
        {
            get
            {
                if (Item.Special != null)
                    return Item.Special.CalculateNewPrice(Item.PurchasePrice, Count);
                return Item.PurchasePrice * Count;
            }
        }

        public EachesGroceryItemOrder(EachesGroceryItem item, int count = 1)
        {
            this.Item = item;
            this.Count = count;
        }
    }
}

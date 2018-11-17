using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class WeighedGroceryItemOrder : IGroceryItemOrder
    {
        public IWeighedGroceryItem Item { get; private set; }
        public decimal Weight { get; set; }

        public decimal Price
        {
            get
            {
                return Item.CalculatePurchasePrice(Weight);
            }
        }

        public WeighedGroceryItemOrder(IWeighedGroceryItem item, decimal weight)
        {
            this.Item = item;
            this.Weight = weight;
        }

    }
}

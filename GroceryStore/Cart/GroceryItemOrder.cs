using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart
{
    public abstract class GroceryItemOrder<TQuantity> : IGroceryItemOrder<TQuantity>
    {
        public IGroceryItem<TQuantity> Item { get; private set; }

        public TQuantity Quantity { get; private set; }

        public decimal Price => Item.CalculatePurchasePrice(Quantity);

        IGroceryItem IGroceryItemOrder.Item => Item;

        public GroceryItemOrder(IGroceryItem<TQuantity> item, TQuantity quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }

        public abstract IGroceryItemOrder Combine(IGroceryItemOrder otherOrder);
    }
}

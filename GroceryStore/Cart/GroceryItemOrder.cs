using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Stock;

namespace GroceryStore.Cart
{
    public abstract class GroceryItemOrder<TUnit> : IGroceryItemOrder<TUnit>
    {
        public IGroceryItem<TUnit> Item { get; private set; }

        public TUnit Quantity { get; private set; }

        public decimal Price => Item.CalculatePurchasePrice(Quantity);

        IGroceryItem IGroceryItemOrder.Item => Item;

        public GroceryItemOrder(IGroceryItem<TUnit> item, TUnit quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }

        public abstract IGroceryItemOrder Combine(IGroceryItemOrder otherOrder);
    }
}

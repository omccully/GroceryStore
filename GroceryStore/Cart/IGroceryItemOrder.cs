using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public interface IGroceryItemOrder
    {
        IGroceryItem Item { get; }
        decimal Price { get; }
        IGroceryItemOrder Combine(IGroceryItemOrder otherOrder);
    }

    public interface IGroceryItemOrder<TQuantity> : IGroceryItemOrder
    {
        new IGroceryItem<TQuantity> Item { get; }
        TQuantity Quantity { get; }
    }
}

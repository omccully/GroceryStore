using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public interface IGroceryItemOrder
    {
        decimal Price { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Specials
{
    public interface IGroceryItemSpecial<T>
    {
        decimal CalculateNewPrice(decimal pricePerItem, T unitsOfItem);
    }
}

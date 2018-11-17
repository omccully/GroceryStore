using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Specials.Weighed
{
    public interface IWeighedGroceryItemSpecial
    {
        decimal CalculateNewPrice(decimal pricePerUnit, decimal itemWeight);
    }
}

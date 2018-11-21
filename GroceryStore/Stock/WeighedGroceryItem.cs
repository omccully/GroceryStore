using GroceryStore.Markdowns;
using GroceryStore.Specials.Weighed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class WeighedGroceryItem : GroceryItem<decimal>
    {
        public WeighedGroceryItem(string name, decimal originalPricePerUnit)
            : base(name, originalPricePerUnit)
        {
           
        }
    }
}

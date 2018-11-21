using GroceryStore.Markdowns;
using GroceryStore.Specials.Eaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class EachesGroceryItem : GroceryItem<int>
    {
        public EachesGroceryItem(string name, decimal originalPrice)
            : base(name, originalPrice)
        {

        }
    }
}

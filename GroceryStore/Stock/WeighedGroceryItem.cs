using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class WeighedGroceryItem
    {
        public string Name { get; private set; }

        public decimal OriginalPrice { get; set; }

        public WeighedGroceryItem(string name, decimal originalPrice)
        {
            this.Name = name;
            this.OriginalPrice = originalPrice;
        }
    }
}

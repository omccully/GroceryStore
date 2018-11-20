using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class GroceryItemScanner
    {
        public List<IGroceryItem> Items;

        public GroceryItemScanner()
        {
            Items = new List<IGroceryItem>();
        }

        public IGroceryItem Scan(string name)
        {
            IEnumerable<IGroceryItem> matches = Items.Where(item => item.Name == name);
            if(matches.Count() == 0) throw new GroceryItemNotFoundException();
            if (matches.Count() > 1) throw new DuplicateGroceryItemException(matches);
            return matches.First();
        }
    }
}

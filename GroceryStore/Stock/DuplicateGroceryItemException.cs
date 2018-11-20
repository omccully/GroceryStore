using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class DuplicateGroceryItemException : Exception
    {
        public IEnumerable<IGroceryItem> Duplicates { get; private set; }

        public DuplicateGroceryItemException(IEnumerable<IGroceryItem> duplicates)
        {
            this.Duplicates = duplicates;
        }
    }
}

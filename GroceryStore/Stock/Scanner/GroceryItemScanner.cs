using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock.Scanner
{
    /// <summary>
    /// Contains the collection of items that the store sells and allows for resolving items by name
    /// </summary>
    public class GroceryItemScanner
    {
        public ICollection<IGroceryItem> Items { get; }

        public IGroceryItemOrderFactory OrderFactory { get; set; }

        public GroceryItemScanner(IGroceryItemOrderFactory orderFactory)
        {
            this.OrderFactory = orderFactory;
            Items = new List<IGroceryItem>();
        }

        public IGroceryItem Scan(string name)
        {
            IEnumerable<IGroceryItem> matches = Items.Where(item => item.Name == name);
            if(matches.Count() == 0) throw new GroceryItemNotFoundException();
            if (matches.Count() > 1) throw new DuplicateGroceryItemException(matches);
            return matches.First();
        }

        public IGroceryItemOrder CreateOrder(string name)
        {
            IGroceryItem item = Scan(name);
            return OrderFactory.CreateOrder(item);
        }
    }
}

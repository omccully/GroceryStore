﻿using GroceryStore.Cart;
using GroceryStore.Cart.OrderFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class GroceryItemScanner
    {
        public List<IGroceryItem> Items { get; set; }

        public IGroceryItemOrderFactory OrderFactory { get; set; }

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

        public IGroceryItemOrder CreateOrder(string name)
        {
            IGroceryItem item = Scan(name);
            return OrderFactory.CreateOrder(item);
        }
    }
}

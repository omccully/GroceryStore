﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class EachesGroceryItem
    {
        public string Name { get; private set; }

        public decimal OriginalPrice { get; set; }

        public EachesGroceryItem(string name, decimal originalPrice)
        {
            this.Name = name;
            this.OriginalPrice = originalPrice;
        }
    }
}

﻿using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart.OrderFactories.CountSelectors
{
    public interface ICountSelector
    {
        int SelectCount(IGroceryItem<int> item);
    }
}

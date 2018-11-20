﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public interface IEachesGroceryItem : IGroceryItem
    {
        decimal CalculatePurchasePrice(int count);
    }
}

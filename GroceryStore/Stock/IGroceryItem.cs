﻿using GroceryStore.Markdowns;
using GroceryStore.Specials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public interface IGroceryItem
    {
        string Name { get; }

        decimal OriginalPrice { get; set; }

        IPriceMarkdown Markdown { get; set; }

        decimal PurchasePrice { get; }
    }

    public interface IGroceryItem<TQuantity> : IGroceryItem
    {
        IGroceryItemSpecial<TQuantity> Special { get; set; }

        decimal CalculatePurchasePrice(TQuantity unitsOfItem);
    }
}

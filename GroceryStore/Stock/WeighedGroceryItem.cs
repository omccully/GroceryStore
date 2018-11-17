using GroceryStore.Markdowns;
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

        public decimal OriginalPricePerUnit { get; set; }

        public IPriceMarkdown Markdown { get; set; }

        public decimal PurchasePricePerUnit
        {
            get
            {
                if (Markdown == null) return OriginalPricePerUnit;
                return Markdown.CalculateNewPrice(OriginalPricePerUnit);
            }
        }

        public WeighedGroceryItem(string name, decimal originalPricePerUnit)
        {
            this.Name = name;
            this.OriginalPricePerUnit = originalPricePerUnit;
        }
    }
}

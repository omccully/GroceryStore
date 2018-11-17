using GroceryStore.Markdowns;
using GroceryStore.Specials.Weighed;
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

        public IWeighedGroceryItemSpecial Special { get; set; }

        public WeighedGroceryItem(string name, decimal originalPricePerUnit)
        {
            this.Name = name;
            this.OriginalPricePerUnit = originalPricePerUnit;
        }

        public decimal CalculatePurchasePrice(decimal weight)
        {
            if (Special != null)
                return Special.CalculateNewPrice(PurchasePricePerUnit, weight);
            return PurchasePricePerUnit * weight;
        }
    }
}

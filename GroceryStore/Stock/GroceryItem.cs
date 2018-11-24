using GroceryStore.Markdowns;
using GroceryStore.Specials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public abstract class GroceryItem<TQuantity> : IGroceryItem<TQuantity>
    {
        public string Name { get; private set; }

        public decimal OriginalPrice { get; set; }

        public IPriceMarkdown Markdown { get; set; }

        public decimal PurchasePrice
        {
            get
            {
                if (Markdown == null) return OriginalPrice;
                return Markdown.CalculateNewPrice(OriginalPrice);
            }
        }

        public IGroceryItemSpecial<TQuantity> Special { get; set; }

        public GroceryItem(string name, decimal originalPrice)
        {
            this.Name = name;
            this.OriginalPrice = originalPrice;
        }

        public decimal CalculatePurchasePrice(TQuantity unitsOfItem)
        {
            if (Special != null)
            {
                return Special.CalculateNewPrice(PurchasePrice, unitsOfItem);
            }

            return PurchasePrice * (dynamic)unitsOfItem;
        }
    }
}

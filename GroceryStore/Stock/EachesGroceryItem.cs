using GroceryStore.Markdowns;
using GroceryStore.Specials.Eaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Stock
{
    public class EachesGroceryItem : IEachesGroceryItem
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

        public IEachesGroceryItemSpecial Special { get; set; }

        public EachesGroceryItem(string name, decimal originalPrice)
        {
            this.Name = name;
            this.OriginalPrice = originalPrice;
        }

        public decimal CalculatePurchasePrice(int count)
        {
            if (Special != null)
                return Special.CalculateNewPrice(PurchasePrice, count);
            return PurchasePrice * count;
        }
    }
}

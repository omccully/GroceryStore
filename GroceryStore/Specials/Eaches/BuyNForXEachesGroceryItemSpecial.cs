using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart;

namespace GroceryStore.Specials.Eaches
{
    public class BuyNForXEachesGroceryItemSpecial : IEachesGroceryItemSpecial
    {
        int RequiredCount;
        decimal FixedPrice;

        public BuyNForXEachesGroceryItemSpecial(int requiredCount, decimal fixedPrice)
        {
            this.RequiredCount = requiredCount;
            this.FixedPrice = fixedPrice;
        }

        public decimal CalculateNewPrice(decimal pricePerItem, int itemCount)
        {
            int countDiscountApplied = itemCount / RequiredCount;
            decimal costForDiscountedItems = countDiscountApplied * FixedPrice;

            int nonDiscountedItemsCount = itemCount % RequiredCount;
            decimal costForNondiscountedItems = nonDiscountedItemsCount * pricePerItem;

            return costForDiscountedItems + costForNondiscountedItems;
        }
    }
}

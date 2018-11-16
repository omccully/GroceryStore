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

        public decimal CalculateNewPrice(EachesGroceryItemOrder itemOrder)
        {
            if (itemOrder.Count < RequiredCount)
                return itemOrder.Count * itemOrder.Item.PurchasePrice;

            if(itemOrder.Count % RequiredCount == 0)
                return FixedPrice * (itemOrder.Count / RequiredCount);

            int countDiscountApplied = itemOrder.Count / RequiredCount;
            decimal costForDiscountedItems = countDiscountApplied * FixedPrice;

            int nonDiscountedItemsCount = itemOrder.Count % RequiredCount;
            decimal costForNondiscountedItems = nonDiscountedItemsCount * itemOrder.Item.PurchasePrice;

            return costForDiscountedItems + costForNondiscountedItems;
        }
    }
}

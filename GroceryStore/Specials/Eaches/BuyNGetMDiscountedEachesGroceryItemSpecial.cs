using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart;

namespace GroceryStore.Specials.Eaches
{
    public class BuyNGetMDiscountedEachesGroceryItemSpecial : IEachesGroceryItemSpecial
    {
        int RequiredCount, DiscountedCount;
        decimal DiscountPercentage;

        public BuyNGetMDiscountedEachesGroceryItemSpecial(int requiredCount, 
            int discountedCount, decimal discountPercentage)
        {
            this.RequiredCount = requiredCount;
            this.DiscountedCount = discountedCount;
            this.DiscountPercentage = discountPercentage;
        }

        public decimal CalculateNewPrice(EachesGroceryItemOrder itemOrder)
        {
            if (itemOrder.Count < RequiredCount)
                return itemOrder.Count * itemOrder.Item.PurchasePrice;

            int itemsPerFullDeal = (RequiredCount + DiscountedCount);
            int numerOfFullDeals = itemOrder.Count / itemsPerFullDeal;
            

            decimal discountPrice = itemOrder.Item.PurchasePrice * DiscountMultiplier;
            decimal discountedItems = numerOfFullDeals * DiscountedCount;
            decimal totalDiscountedPrice = discountPrice * discountedItems;

            decimal nondiscountedItems = numerOfFullDeals * RequiredCount;
            decimal totalNondiscountedPrice = nondiscountedItems * itemOrder.Item.PurchasePrice;

            if (itemOrder.Count % itemsPerFullDeal == 0)
            {
                return totalDiscountedPrice + totalNondiscountedPrice;
            }

            int remainingItemsCount = itemOrder.Count % itemsPerFullDeal;
            decimal remainingItemsPrice = remainingItemsCount * itemOrder.Item.PurchasePrice;

            totalNondiscountedPrice += remainingItemsPrice;

            return totalDiscountedPrice + totalNondiscountedPrice;
        }

        decimal DiscountMultiplier
        {
            get
            {
                return 1M - (DiscountPercentage / 100M);
            }
        }
    }
}

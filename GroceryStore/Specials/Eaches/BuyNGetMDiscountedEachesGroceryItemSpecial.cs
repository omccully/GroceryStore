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

            int saleItems = (RequiredCount + DiscountedCount);
            if (itemOrder.Count % saleItems == 0)
            {
                int sales = itemOrder.Count / saleItems;

                decimal discountPrice = itemOrder.Item.PurchasePrice * DiscountMultiplier;
                decimal discountedItems = sales * DiscountedCount;
                decimal totalDiscountedPrice = discountPrice * discountedItems;

                decimal nondiscountedItems = sales * RequiredCount;
                decimal totalNondiscountedPrice = nondiscountedItems * itemOrder.Item.PurchasePrice;

                return totalDiscountedPrice + totalNondiscountedPrice;
            }

            return 0M;
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

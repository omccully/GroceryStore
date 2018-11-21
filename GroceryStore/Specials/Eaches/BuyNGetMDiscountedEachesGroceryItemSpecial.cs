using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart;

namespace GroceryStore.Specials.Eaches
{
    public class BuyNGetMDiscountedEachesGroceryItemSpecial : IGroceryItemSpecial<int>
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

        public decimal CalculateNewPrice(decimal pricePerItem, int itemCount)
        {
            decimal discountedItemsPrice = CaculateDiscountedItemCount(itemCount) *
                (pricePerItem * DiscountMultiplier);

            decimal nondiscountedItemsPrice = CaculateNondiscountedItemCount(itemCount) *
                pricePerItem;

            return discountedItemsPrice + nondiscountedItemsPrice;
        }

        int CaculateDiscountedItemCount(int orderedItemsCount)
        {
            return orderedItemsCount - CaculateNondiscountedItemCount(orderedItemsCount);
        }

        int CaculateNondiscountedItemCount(int orderedItemsCount)
        {
            int remainingItemCount;
            int numberOfFullDeals = CalculateNumberOfFullDeals(orderedItemsCount, out remainingItemCount);
            int nondiscountedItemsFromFullDeals = numberOfFullDeals * RequiredCount;

            // if DiscountedItemsAreFree, then the customer doesn't HAVE 
            // to take all of the free items offered to them,
            // so charge them for a maximum of RequiredCount from the remaining items
            // else, charge the customer the full price for the remaining items
            int nondiscountedItemsFromRemaining =
                DiscountedItemsAreFree ? Math.Min(RequiredCount, remainingItemCount) : remainingItemCount;

            return nondiscountedItemsFromFullDeals + nondiscountedItemsFromRemaining;
        }

        int CalculateNumberOfFullDeals(int orderedItemsCount, out int remainingItemsCount)
        {
            remainingItemsCount = orderedItemsCount % ItemsPerFullDeal;

            return orderedItemsCount / ItemsPerFullDeal;
        }
        

        bool DiscountedItemsAreFree
        {
            get
            {
                return DiscountPercentage == 100M;
            }
        }

        int ItemsPerFullDeal
        {
            get
            {
                return (RequiredCount + DiscountedCount);
            }
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

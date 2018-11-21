using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart;

namespace GroceryStore.Specials.Eaches
{
    public class LimitedEachesGroceryItemSpecial : IGroceryItemSpecial<int>
    {
        IGroceryItemSpecial<int> InnerSpecial;
        int Limit;

        public LimitedEachesGroceryItemSpecial(IGroceryItemSpecial<int> innerSpecial, int limit)
        {
            this.InnerSpecial = innerSpecial;
            this.Limit = limit;
        }

        public decimal CalculateNewPrice(decimal pricePerItem, int itemCount)
        {
            int numItemsToApplySpecialTo = Math.Min(Limit, itemCount);

            decimal priceOfItemsWithSpecialApplied = 
                InnerSpecial.CalculateNewPrice(pricePerItem, numItemsToApplySpecialTo);

            int numItemsNotToApplySpecialTo = itemCount - numItemsToApplySpecialTo;
            decimal priceOfItemsWithoutSpecialApplied = 
                numItemsNotToApplySpecialTo * pricePerItem;

            return priceOfItemsWithSpecialApplied + priceOfItemsWithoutSpecialApplied;
        }
    }
}

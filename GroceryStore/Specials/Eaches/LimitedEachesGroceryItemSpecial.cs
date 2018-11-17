using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Cart;

namespace GroceryStore.Specials.Eaches
{
    public class LimitedEachesGroceryItemSpecial : IEachesGroceryItemSpecial
    {
        IEachesGroceryItemSpecial InnerSpecial;
        int Limit;

        public LimitedEachesGroceryItemSpecial(IEachesGroceryItemSpecial innerSpecial, int limit)
        {
            this.InnerSpecial = innerSpecial;
            this.Limit = limit;
        }

        public decimal CalculateNewPrice(EachesGroceryItemOrder itemOrder)
        {
            int numItemsToApplySpecialTo = Math.Min(Limit, itemOrder.Count);
            EachesGroceryItemOrder splitOrder = 
                new EachesGroceryItemOrder(itemOrder.Item, numItemsToApplySpecialTo);
            decimal priceOfItemsWithSpecialApplied = InnerSpecial.CalculateNewPrice(splitOrder);

            int numItemsNotToApplySpecialTo = itemOrder.Count - numItemsToApplySpecialTo;
            decimal priceOfItemsWithoutSpecialApplied = 
                numItemsNotToApplySpecialTo * itemOrder.Item.PurchasePrice;

            return priceOfItemsWithSpecialApplied + priceOfItemsWithoutSpecialApplied;
        }
    }
}

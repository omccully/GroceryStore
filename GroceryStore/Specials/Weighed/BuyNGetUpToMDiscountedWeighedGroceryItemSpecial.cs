using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Specials.Weighed
{
    public class BuyNGetUpToMDiscountedWeighedGroceryItemSpecial
    {
        decimal RequiredWeight;
        decimal DiscountPercentage;

        public BuyNGetUpToMDiscountedWeighedGroceryItemSpecial(
            decimal requiredWeight, decimal discountPercentage)
        {
            this.RequiredWeight = requiredWeight;
            this.DiscountPercentage = discountPercentage;
        }

        public decimal CalculateNewPrice(decimal pricePerUnit, decimal weight)
        {
            return pricePerUnit * weight;
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

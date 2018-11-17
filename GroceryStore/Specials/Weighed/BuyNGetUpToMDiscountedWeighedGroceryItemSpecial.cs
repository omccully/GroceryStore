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

        public decimal CalculateNewPrice(decimal pricePerUnit, decimal itemWeight)
        {
            decimal discountPrice = pricePerUnit * DiscountMultiplier;

            if (itemWeight % WeightPerFullDeal == 0)
            {
                decimal fullDealCount = Math.Round(itemWeight / WeightPerFullDeal);

                decimal fullDealNondiscountedWeight = RequiredWeight * fullDealCount;
                decimal fullDealNondiscountedPrice = fullDealNondiscountedWeight * pricePerUnit;

                decimal fullDealDiscountedWeight = fullDealNondiscountedWeight;
                decimal fullDealDiscountedPrice = fullDealDiscountedWeight * discountPrice;


                return fullDealNondiscountedPrice + fullDealDiscountedPrice;
            }
            
            return pricePerUnit * itemWeight;
        }

        decimal WeightPerFullDeal
        {
            get
            {
                return RequiredWeight * 2;
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

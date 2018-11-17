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

            decimal fullDealCount = Math.Floor(itemWeight / WeightPerFullDeal);

            decimal extraWeight = itemWeight - (fullDealCount * WeightPerFullDeal);
            decimal extraNondiscountedWeight = Math.Min(extraWeight, RequiredWeight);

            decimal fullDealNondiscountedWeight = RequiredWeight * fullDealCount;
            //decimal fullDealNondiscountedPrice = fullDealNondiscountedWeight * pricePerUnit;

            decimal extraDiscountedWeight = extraWeight - extraNondiscountedWeight;
            decimal fullDealDiscountedWeight = fullDealNondiscountedWeight;
            //decimal fullDealDiscountedPrice = fullDealDiscountedWeight * discountPrice;

            decimal nondiscountedPrice = 
                (extraNondiscountedWeight + fullDealNondiscountedWeight) * pricePerUnit;
            decimal discountedPrice =
                (extraDiscountedWeight + fullDealDiscountedWeight) * discountPrice;

            return nondiscountedPrice + discountedPrice;
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

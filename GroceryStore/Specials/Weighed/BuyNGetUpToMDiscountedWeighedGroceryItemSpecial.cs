using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Specials.Weighed
{
    public class BuyNGetUpToMDiscountedWeighedGroceryItemSpecial : IGroceryItemSpecial<decimal>
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
            return CalculateNondiscountedTotalPrice(pricePerUnit, itemWeight) +
               CalculateDiscountedTotalPrice(pricePerUnit, itemWeight);
        }

        decimal CalculateNondiscountedTotalPrice(decimal pricePerUnit, decimal itemWeight)
        {
            decimal nondiscountedWeight = CalculateNondiscountedWeight(itemWeight);
            return nondiscountedWeight * pricePerUnit;
        }

        decimal CalculateDiscountedTotalPrice(decimal pricePerUnit, decimal itemWeight)
        {
            decimal discountedPrice = pricePerUnit * DiscountMultiplier;
            decimal discountedWeight = CalculateDiscountedWeight(itemWeight);
            return discountedWeight * discountedPrice;
        }

        decimal CalculateDiscountedWeight(decimal itemWeight)
        {
            return itemWeight - CalculateNondiscountedWeight(itemWeight);
        }

        decimal CalculateNondiscountedWeight(decimal itemWeight)
        {
            decimal fullDealCount = Math.Floor(itemWeight / WeightPerFullDeal);
            decimal extraWeight = itemWeight - (fullDealCount * WeightPerFullDeal);

            decimal fullDealNondiscountedWeight = RequiredWeight * fullDealCount;
            decimal extraNondiscountedWeight = Math.Min(extraWeight, RequiredWeight);

            return fullDealNondiscountedWeight + extraNondiscountedWeight;
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

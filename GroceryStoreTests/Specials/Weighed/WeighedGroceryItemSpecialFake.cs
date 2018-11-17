using GroceryStore.Specials.Weighed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreTests.Specials.Weighed
{
    class WeighedGroceryItemSpecialFake : IWeighedGroceryItemSpecial
    {
        decimal FixedPricePerUnit;

        public WeighedGroceryItemSpecialFake(decimal fixedPricePerUnit)
        {
            this.FixedPricePerUnit = fixedPricePerUnit;
        }

        public decimal CalculateNewPrice(decimal pricePerUnit, decimal itemWeight)
        {
            return itemWeight * FixedPricePerUnit;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Specials;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    class EachesGroceryItemSpecialFake : IGroceryItemSpecial<int>
    {
        decimal FixedPrice;

        public EachesGroceryItemSpecialFake(decimal fixedPrice)
        {
            this.FixedPrice = fixedPrice;
        }

        public decimal CalculateNewPrice(decimal pricePerItem, int itemCount)
        {
            return itemCount * FixedPrice;
        }
    }
}

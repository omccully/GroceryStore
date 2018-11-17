using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryStore.Specials.Eaches;

namespace GroceryStoreTests.Specials.Eaches
{
    class EachesGroceryItemSpecialFake : IEachesGroceryItemSpecial
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

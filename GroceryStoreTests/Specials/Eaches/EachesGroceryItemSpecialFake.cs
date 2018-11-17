using GroceryStore.Cart;
using GroceryStore.Specials.Eaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

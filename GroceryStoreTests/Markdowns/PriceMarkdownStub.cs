using GroceryStore.Markdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreTests.Markdowns
{
    class PriceMarkdownStub : IPriceMarkdown
    {
        decimal ReturnPrice;

        public PriceMarkdownStub(decimal returnPrice)
        {
            this.ReturnPrice = returnPrice;
        }

        public decimal CalculateNewPrice(decimal originalPrice)
        {
            return ReturnPrice;
        }
    }
}

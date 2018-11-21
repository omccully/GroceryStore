using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Markdowns
{
    /// <summary>
    /// Represents a price markdown that reduces the price by a static amount.
    /// </summary>
    public class PriceMarkdown : IPriceMarkdown
    {
        decimal ReductionAmount;

        public PriceMarkdown(decimal reductionAmount)
        {
            this.ReductionAmount = reductionAmount;
        }

        public decimal CalculateNewPrice(decimal originalPrice)
        {
            return originalPrice - ReductionAmount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class GroceryCart
    {
        public List<IGroceryItemOrder> Orders { get; set; }

        public GroceryCart()
        {
            Orders = new List<IGroceryItemOrder>();
        }
    }
}

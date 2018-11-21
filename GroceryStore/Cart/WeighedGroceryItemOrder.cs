﻿using GroceryStore.Cart.OrderFactories;
using GroceryStore.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Cart
{
    public class WeighedGroceryItemOrder : IGroceryItemOrder
    {
        public IGroceryItem<decimal> Item { get; private set; }
        public decimal Weight { get; set; }

        public decimal Price
        {
            get
            {
                return Item.CalculatePurchasePrice(Weight);
            }
        }

        IGroceryItem IGroceryItemOrder.Item => Item;

        public WeighedGroceryItemOrder(IGroceryItem<decimal> item, decimal weight)
        {
            this.Item = item;
            this.Weight = weight;
        }

        public IGroceryItemOrder Combine(IGroceryItemOrder otherOrder)
        {
            return Combine((WeighedGroceryItemOrder)otherOrder);
        }

        public WeighedGroceryItemOrder Combine(WeighedGroceryItemOrder otherOrder)
        {
            if (Item != otherOrder.Item)
                throw new DifferingItemsException();

            return new WeighedGroceryItemOrder(Item, Weight + otherOrder.Weight);
        }
    }
}

# Checkout Order Total Kata

This library is designed to be unit-agnostic for both curreny and weight. Prices and weights are represented by the decimal data type to prevent rounding issues. 

## Creating an eaches item
```csharp
EachesGroceryItem soup = new EachesGroceryItem("Canned soup", 1.89M);
```

## Markdowns
Markdowns are represented by the ```IPriceMarkdown``` interface. The PriceMarkdown class is provided to allow for a simple subtraction markdown. Markdowns can be applied to a grocery item like this:

```csharp
// mark down price by 0.20
soup.Markdown = new PriceMarkdown(0.20M);
```

Hiding the implementation for the markdown behind an interface allows for some interesting possibilities for the future. For example, we could implement the IPriceMarkdown interface in a way that would only apply a markdown during certain days of the week. The best way to implement this would be to use the decorator pattern and construct them like this:

```csharp
// mark down price by 0.20 only on week days
soup.Markdown = new WeekdayMarkdown(new PriceMarkdown(0.20M));
```

## Specials for eaches items

Specials for eaches items implement the  ```IGroceryItemSpecial<int>``` interface. Specials can be applied to an eaches grocery item by setting the
```Special``` property on an EachesGroceryItem. 
### Buy N for X
```csharp
// buy 3 for 5.00
soup.Special = new BuyNForXEachesGroceryItemSpecial(3, 5.00M);
```
### Buy N Get M X% off
```csharp
// buy 1 get 1 free (100% off)
soup.Special = 
    new BuyNGetMDiscountedEachesGroceryItemSpecial(1, 1, 100M);
```
```csharp
// buy 2 get 1 50% off
soup.Special = 
    new BuyNGetMDiscountedEachesGroceryItemSpecial(2, 1, 50M);
```

### Limited special decorator
```csharp
// buy 3 for 5.00
IGroceryItemSpecial<int> innerSpecial = 
    new BuyNForXEachesGroceryItemSpecial(3, 5.00M);
    
// only apply the innerSpecial to the first 6 cans
soup.Special = new LimitedEachesGroceryItemSpecial(innerSpecial, 6);
```

## Creating a weighed item

```csharp
WeighedGroceryItem bananas = new WeighedGroceryItem("Bananas", 2.38M);
```
The same IPriceMarkdown interface that is used in the EachesGroceryItem class is used in the WeighedGroceryItem class. Markdowns can be set for weighed items by setting the ```Markdown``` property for the item.

## Specials for weighed items

Specials for weighed items implement the  ```IGroceryItemSpecial<decimal>``` interface and can be applied to a weighed item by setting the ```Special``` property. 

### Buy N get M of equal or lesser value X% off
```csharp
// buy 3.0 units, get up to 3.0 units 70% off
bananas.Special = 
    new BuyNGetMOfEqualOrLesserValueDiscountedWeighedGroceryItemSpecial(
        3.0M, 70M);
```
## Putting it all together

### Configuration
```csharp
// setup the store's items 
GroceryItemScanner scanner = new GroceryItemScanner();
scanner.Items.Add(new EachesGroceryItem("Canned soup", 1.89M));
scanner.Items.Add(new WeighedGroceryItem("Bananas", 2.38M));
scanner.Items.Add(new EachesGroceryItem("Bread", 2.49M) 
{
    // price reduced by 0.30
    Markdown = new PriceMarkdown(30)
});
scanner.Items.Add(new EachesGroceryItem("Coconut date rolls", 6.99M) 
{
    // 3 for 15.00
    Special = new BuyNForXEachesGroceryItemSpecial(3, 15.00M)
});

// UiWeightSelector is an example; it's not defined in the GroceryStore library
IWeightSelector uiWeightSelector = new UiWeightSelector() 
scanner.OrderFactory = new AggregateGroceryItemOrderFactory(
        new EachesGroceryItemOrderFactory(),
        new WeighedGroceryItemOrderFactory(uiWeightSelector)
    );
```
### Scanning items, removing items, and checking the total price
```csharp
// creating a checkout cart 
CheckoutCart cart = new CheckoutCart();

// this call could prompt the cashier to weight the item 
// (using the IWeightSelector injected into the WeighedGroceryItemOrderFactory)
IGroceryItemOrder bananaOrder = scanner.CreateOrder("Bananas");

// add to cart
cart.Orders.Add(bananaOrder);

// check price including bananas
decimal price = cart.TotalPrice;

// remove the added banana order
cart.Orders.Remove(bananaOrder);

// check price without bananas
decimal price = cart.TotalPrice;
```
### Alternative way to scan items
```csharp 
// bypassing the order creation factories
// use this if the weight of the items are known ahead of time
// and you're just using this library for checking prices
IGroceryItem item = scanner.Scan("Bananas");
IGroceryItemOrder order = null;
if(item is WeighedGroceryItem) 
{
    decimal weightInput = PromptUserForWeight();
    order = new WeighedGroceryItemOrder((WeighedGroceryItem)item, weightInput);
}
else if(item is EachesGroceryItem) 
{
    order = new EachesGroceryItemOrder((EachesGroceryItem)item, 1);
}
else 
{
    throw new Exception("Item is an unrecognized type");
}
cart.Orders.Add(order);
```
## Running the tests

All of the software necessary to run the tests for this library are included in Visual Studio. The easiest way to run the tests is to:
1.  Open the project in Visual Studio
2. Go to Test -> Windows -> Test Explorer in the toolbar at the top of Visual studio
3. In the Test Explorer, click "Run All"
4. Once the tests are finished, a green checkmark should appear next to each test, signifying that all of the tests passed.

To run the tests from a command prompt (also requires Visual Studio to be installed):
1. Find msbuild.exe. Example: ```C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe```
2. In a command prompt, change directory to the GroceryStoreTests project folder
3. Compile the test project: ```"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe" GroceryStoreTests.csproj```
4. Find vstest.console.exe. Example: ```C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe```
5. In a command prompt, change directory to the GroceryStoreTests/bin/Debug folder. 
6. Run the tests: ```"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" GroceryStoreTests.dll```

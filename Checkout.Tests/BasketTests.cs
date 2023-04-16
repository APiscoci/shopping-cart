
namespace Checkout.Tests;

public class BasketTests
{
	
	private readonly Basket _basket;
	
	public BasketTests()
	{
		_basket = new Basket();
	}
	
	[Fact]
	public void ShouldTotal0WhenBasketIsEmpty()
	{
		// When
		var total = _basket.GetTotalBasketCost();
		
		// Then
		Assert.Equal(total, 0);

	}
	
	[Fact]
	public void ShouldAddProductsToBasket()
	{
		// Given
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "A" , UnitPrice = 10m, QuantityPerProduct = 1 });
		
		// When 
		var total = _basket.GetTotalBasketCost();
		
		// Then 
		Assert.Equal(total, 10);
	}
}
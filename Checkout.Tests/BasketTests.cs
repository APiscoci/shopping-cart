
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
		Assert.Equal(0, total);

	}
	
	[Theory]
	[InlineData(1, 10)]
	[InlineData(2, 20)]
	public void ShouldAddProductsToBasket(int quantity, decimal expectedPrice)
	{
		// Given
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "A" , UnitPrice = 10m, QuantityPerProduct = quantity });
		
		// When 
		var total = _basket.GetTotalBasketCost();
		
		// Then 
		Assert.Equal(expectedPrice, total);
	}
	
	
	[Theory]
	[InlineData(1, 50)]
	[InlineData(2, 100)]
	public void ShouldAddDiferentProductsToBasket (int quantity, decimal expectedPrice)
	{
		// Given
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "A" , UnitPrice = 10m, QuantityPerProduct = quantity });
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "C" , UnitPrice = 40m, QuantityPerProduct = quantity });
		
			// When 
		var total = _basket.GetTotalBasketCost();
		
		// Then 
		Assert.Equal(expectedPrice, total);
	}
	
	
	[Fact]
	public void SHouldApplyQuantityPromotionToItemB()
	{
		// Given
		_basket.AddProductToBasket(new BasketProduct{StockKeepingUnit = "B", UnitPrice = 15m, QuantityPerProduct = 3});

		// When
		var total = _basket.GetTotalBasketCost();

		// Then
		Assert.Equal(40, total);
	}
	
	
}
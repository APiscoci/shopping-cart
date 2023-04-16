
namespace Checkout.Tests;

public class BasketTests
{
	
	private readonly Basket _basket;
	
	public BasketTests()
	{
		var quantityPromotion = new QuantityPromotion("B", 3, 40m);
		var promotions = new List<Promotion>{quantityPromotion};
		_basket = new Basket(promotions);
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
	
	
	[Theory]
	[InlineData(1, 15)]
	[InlineData(2, 30)]
	[InlineData(3, 40)]
	[InlineData(4, 55)]
	[InlineData(5, 70)]
	[InlineData(6, 80)]
	
	public void SHouldApplyQuantityPromotionToItemB(int quantity, decimal expectedTotal)
	{
		// Given
		_basket.AddProductToBasket(new BasketProduct{StockKeepingUnit = "B", UnitPrice = 15m, QuantityPerProduct = quantity});

		// When
		var total = _basket.GetTotalBasketCost();

		// Then
		Assert.Equal(total, expectedTotal);
	}
	
	
}
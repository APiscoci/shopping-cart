
namespace Checkout.Tests;

public class BasketTests
{
	
	private readonly Basket _basket;
	
	public BasketTests()
	{
		var quantityPromotion = new QuantityPromotion("B", 3, 40m);
		var percentagePromotion = new PercentagePromotion("D", 2, 25);
		var promotions = new List<Promotion>{quantityPromotion, percentagePromotion};
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
	public void ShouldApplyQuantityPromotionToProductB(int quantity, decimal expectedTotal)
	{
		// Given
		_basket.AddProductToBasket(new BasketProduct{StockKeepingUnit = "B", UnitPrice = 15m, QuantityPerProduct = quantity});

		// When
		var total = _basket.GetTotalBasketCost();

		// Then
		Assert.Equal(total, expectedTotal);
	}
	
	[Theory]
	[InlineData(1, 55)]
	[InlineData(2, 82.5)]
	[InlineData(3, 137.5)]
	[InlineData(4, 165)]
	[InlineData(5, 220)]
	[InlineData(6, 247.5)]
	public void ShouldApplyQuantityPromotionToProductD(int quantity, decimal expectedTotal)
	{
		// Given
		_basket.AddProductToBasket(new BasketProduct{StockKeepingUnit = "D", UnitPrice = 55m, QuantityPerProduct = quantity});

		// When
		var total = _basket.GetTotalBasketCost();

		// Then
		Assert.Equal(total, expectedTotal);
	}
	
	[Fact]
	public void ShouldAddAllProductsToBasket ()
	{
		// Given
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "A" , UnitPrice = 10m, QuantityPerProduct = 1 });
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "B" , UnitPrice = 15m, QuantityPerProduct = 3 });
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "C" , UnitPrice = 40m, QuantityPerProduct = 2 });
		_basket.AddProductToBasket(
			new BasketProduct{StockKeepingUnit = "D" , UnitPrice = 55m, QuantityPerProduct = 3 });
		
			// When 
		var total = _basket.GetTotalBasketCost();
		
		// Then 
		Assert.Equal(267.5m, total);
	}
}
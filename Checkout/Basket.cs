using System.Collections.Generic;
using System.Linq;

namespace Checkout 
{
	public class Basket 
	{
		private readonly ICollection<BasketProduct> _products;
		
		public Basket()
		{
			_products = new List<BasketProduct>();
		}
		
		public void AddProductToBasket(BasketProduct basketProduct)
		{
			_products.Add(basketProduct);
		}
		
		public decimal GetTotalBasketCost()
		{
			var total = _products.Sum(product => product.UnitPrice);
			return total;
		}
		
	}
}
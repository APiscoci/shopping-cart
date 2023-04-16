using System.Collections.Generic;
using System.Linq;

namespace Checkout 
{
	public class Basket 
	{
		private readonly ICollection<BasketProduct> _products;
		private readonly IEnumerable<Promotion> _promotions;
		
		public Basket( IEnumerable<Promotion> promotions)
		{
			_products = new List<BasketProduct>();
			_promotions = promotions;
		}
		
		public void AddProductToBasket(BasketProduct basketProduct)
		{
			_products.Add(basketProduct);
		}
		
		public decimal GetTotalBasketCost()
		{
			var total = _products.Sum(product => product.UnitPrice * product.QuantityPerProduct);
			
			foreach(var promotion in _promotions)
			{
				var reduction  = promotion.GetReduction(_products);
				total-= reduction;
			}
			
			return total;
		}
		
	}
}
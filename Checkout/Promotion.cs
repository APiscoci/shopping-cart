 using System.Collections.Generic;
 using System.Linq;
 
 namespace Checkout
 {
 	public abstract class Promotion
	{
		private readonly string _stockKeepingUnit;
		private readonly int _requiredQuantity;
		
		public Promotion( string stockKeepingUnit, int requiredQuantity)
		{
			_stockKeepingUnit = stockKeepingUnit;
			_requiredQuantity = requiredQuantity;
		}
		
		public decimal GetReduction(IEnumerable<BasketProduct> products)
		{
			var promotionProducts = products.Where(x => x.StockKeepingUnit == _stockKeepingUnit);
			var numberOfProducts = promotionProducts.Sum(x => x.QuantityPerProduct);
			
			if(numberOfProducts == 0 )
			{
				return 0;
			}
			
			var numberOfPromotions = numberOfProducts / _requiredQuantity;
		
			var totalPriceWithoutPromotion = GetTotalPriceWthPromotion(numberOfProducts, promotionProducts.First().UnitPrice);
			
			var reductionPerPromotion = CalculatePromotion(totalPriceWithoutPromotion);
			
			var reduction = numberOfPromotions * reductionPerPromotion;
			return reduction;
		}
		
		private decimal GetTotalPriceWthPromotion(int numberOfProducts, decimal unitPrice)
		{
			var numberOfPromotions = numberOfProducts / _requiredQuantity;
		
			return _requiredQuantity * unitPrice;
		}
		
		protected abstract decimal CalculatePromotion(decimal productsPrice);
	}
 }
namespace Checkout
{
	public class QuantityPromotion : Promotion
	{
		private readonly decimal _finalPrice;
		
		public QuantityPromotion(string stockKeepingUnit, int requiredQuantity, decimal finalPrice) : base ( stockKeepingUnit, requiredQuantity)
		{
			_finalPrice = finalPrice;
		} 
		
		protected override decimal CalculatePromotion(decimal productsPrice)
		{
			return productsPrice - _finalPrice;
		}
	}
}
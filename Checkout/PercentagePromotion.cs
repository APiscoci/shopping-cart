namespace Checkout
{
	public class PercentagePromotion : Promotion
	{
		private readonly decimal _percentageValue;
		
		public PercentagePromotion(string stockKeepingUnit, int requiredQuantity, decimal percentageValue) : base ( stockKeepingUnit, requiredQuantity)
		{
			_percentageValue = percentageValue; 
		}
		
		protected override decimal CalculatePromotion(decimal finalPrice)
		{
			return finalPrice * _percentageValue /100m;
		}
	}
}
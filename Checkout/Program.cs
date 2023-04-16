
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Checkout
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("products.json"));
			
			var quantityPromotion = new QuantityPromotion("B", 3, 40m);
			var percentagePromotion = new PercentagePromotion("D", 2, 25m);
			var promotions = new List<Promotion>{quantityPromotion, percentagePromotion};
			
			Basket basket = new Basket(promotions);
			
			Console.WriteLine("<=== Shopping Cart ===>");
		 	Console.WriteLine(" ");
			Console.WriteLine("Type 'Exit' when you are done shopping!");
			Console.WriteLine(" ");
			Console.WriteLine("Available products:");
			
			foreach(Product product in products)
			{
				Console.WriteLine($"Product: {product.StockKeepingUnit}, Price: {product.UnitPrice}");
			}
			
			Console.WriteLine(" ");
			
			while(true)
			{
				Console.WriteLine("What product would you like to add?");
				var selectedProduct = Console.ReadLine().ToString().ToLower();
				var productAvailable = products.Find(product => product.StockKeepingUnit.ToLower() == selectedProduct);
				
				if(productAvailable != null)
				{
				
					Console.WriteLine("What quantity?");
					var quantity = Math.Max(1, Convert.ToInt32(Console.ReadLine()));
					
					var productToBasket = new BasketProduct
					{
						StockKeepingUnit = productAvailable.StockKeepingUnit,
						UnitPrice = productAvailable.UnitPrice,
						QuantityPerProduct = quantity
					};
					
					basket.AddProductToBasket(productToBasket);
					Console.WriteLine($"Product Added! Total Cost: {basket.GetTotalBasketCost()}");
					Console.WriteLine(" ");
				}
				else
				{
					if(selectedProduct == "exit")
					{
						Console.WriteLine(" ");
						Console.WriteLine($"Total Cost: {basket.GetTotalBasketCost()}");
						Console.WriteLine("Thank you for your purchase!");
						
						break;
					}
					else
					{
						Console.WriteLine("Product does not exist. Try again!");
					}
				}
				
			}
			
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket;

namespace ShoppingBasketApp
{
	internal class Program
	{
		#region CONST
		
		private const string SPACING = "\t";
		private const string DOLLAR_SIGN = "$";
		private const string HEADER = "Product" + Program.SPACING + "Price" + Program.SPACING + "Quantity" + Program.SPACING + "Amount" + Program.SPACING + "Applied discount";
		private const string SEPARATOR = "---------------------------------------------------------";

		#endregion

		public static void Main(string[] args)
		{
			InitialiseBasketItems();
			InitialiseDiscounts();
			PrintProductList();
			AddToBasket();
			PrintBasketContent();
		}

		#region Display/Print

		private static void PrintProductList()
		{
			List<Product> availableProducts = ShoppingBasket.ShoppingBasket.GetAvailableProducts();
			foreach(Product availableProduct in availableProducts)
			{
				Console.WriteLine(availableProducts.IndexOf(availableProduct) + 1 + ":  " + availableProduct.Name + ":  $" + availableProduct.Price);
			}
		}

		private static void PrintBasketContent()
		{
			PrintTableHeader();
			List<Product> basket = ShoppingBasket.ShoppingBasket.GetBasketedProducts();
			decimal discount = ShoppingBasket.ShoppingBasket.CalcualteDiscount();
			string finalText = Program.HEADER + Environment.NewLine;
			foreach(Product basketedProduct in basket.Distinct())
			{
				decimal? discountPerGroup = ShoppingBasket.ShoppingBasket.GetDiscounts().FirstOrDefault(x => x.TargetProduct.Name == basketedProduct.Name)?.DiscountApplicable;
				discountPerGroup ??= 0.0m;
				string displayText = basketedProduct.Name //
											+ Program.SPACING //
											+ Program.DOLLAR_SIGN // 
											+ basketedProduct.Price //
											+ Program.SPACING //
											+ Program.SPACING // 
											+ basket.Count(x => x.Name == basketedProduct.Name) //
											+ Program.SPACING //
											+ Program.DOLLAR_SIGN // 
											+ basket.Count(x => x.Name == basketedProduct.Name) * basketedProduct.Price //
											+ Program.SPACING //
											+ Program.DOLLAR_SIGN // 
											+ discountPerGroup;
				Console.WriteLine(displayText);
				finalText += displayText + Environment.NewLine;
			}

			PrintSeparator();
			decimal basketTotal = ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmount();
			Console.WriteLine("Basket total " + basketTotal + Program.DOLLAR_SIGN);
			finalText += "Basket total " + basketTotal + Program.DOLLAR_SIGN + Environment.NewLine;
			Console.WriteLine("Basket discount: " + discount + Program.DOLLAR_SIGN);
			finalText += "Basket discount: " + discount + Program.DOLLAR_SIGN + Environment.NewLine;
			PrintSeparator();
			Console.WriteLine("Basket total with discount: " + ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount() + Program.DOLLAR_SIGN);
			finalText += "Basket total with discount: " + ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount() + Program.DOLLAR_SIGN;
			Logger.LogInfo(finalText + Environment.NewLine);
		}

		private static void PrintSeparator()
		{
			Console.WriteLine(Program.SEPARATOR);
		}

		private static void PrintTableHeader()
		{
			Console.WriteLine(Program.HEADER);
		}

		#endregion

		#region Initialize Products and Discounts

		private static void InitialiseBasketItems()
		{
			//This way the available products can be imported from another source
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Butter", 0.80m);
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Milk", 1.15m);
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Bread", 1);
		}

		private static void InitialiseDiscounts()
		{
			ShoppingBasket.ShoppingBasket.AddDiscount(ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Milk"), 4,
																	ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Milk"), 100);

			ShoppingBasket.ShoppingBasket.AddDiscount(ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Butter"), 2,
																	ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Bread"), 50);
		}

		#endregion

		#region Basket Management

		private static void AddToBasket()
		{
			int availableProducts = ShoppingBasket.ShoppingBasket.GetAvailableProducts().Count;
			Console.WriteLine("Enter product ID");
			string productID = Console.ReadLine();
			if(int.TryParse(productID, out int parsedInput)
				&& parsedInput <= availableProducts)
			{
				ShoppingBasket.ShoppingBasket.AddProductToBasket(parsedInput);
				AddToBasket();
			}
			else
			{
				Console.WriteLine("Product not in list. Enter A to add more items to the basket or F to finish adding and to calculate the total");
				if(Console.ReadLine()?.ToLower() == "a")
				{
					AddToBasket();
				}
				else if(Console.ReadLine()?.ToLower() == "f")
				{
					return;
				}
			}
		}

		#endregion
	}
}
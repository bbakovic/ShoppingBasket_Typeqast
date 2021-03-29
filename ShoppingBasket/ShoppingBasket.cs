using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
	public class ShoppingBasket
	{
		#region Locals

		private static readonly List<Product> availableProducts = new List<Product>();
		private static readonly List<Product> basketedProducts = new List<Product>();
		private static readonly List<Discount> discounts = new List<Discount>();

		#endregion

		#region Calculations

		public static decimal CalculateShoppingBasketAmountWithDiscount()
		{
			return ShoppingBasket.basketedProducts.Sum(product => product.Price) - CalcualteDiscount();
		}

		public static decimal CalculateShoppingBasketAmount()
		{
			return ShoppingBasket.basketedProducts.Sum(product => product.Price);
		}

		public static decimal CalcualteDiscount()
		{
			decimal disAmount = 0;
			foreach(Discount discount in ShoppingBasket.discounts)
			{
				List<Product> sourceCount = ShoppingBasket.basketedProducts.Where(x => x.Name == discount.SourceProduct.Name).ToList();
				if(sourceCount.Count >= discount.RequiredAmmount)
				{
					List<Product> targetCount = ShoppingBasket.basketedProducts.Where(x => x.Name == discount.TargetProduct.Name).ToList();
					int discountApplicableOn = sourceCount.Count / discount.RequiredAmmount; //discount.SourceProduct.Name != discount.TargetProduct.Name
					//? (sourceCount.Count / discount.RequiredAmmount)
					//: sourceCount.Count / discount.RequiredAmmount;

					decimal discountPerItemGroup = (targetCount.FirstOrDefault()?.Price ?? 0) * ((discount.DiscountDefined / 100) * (discountApplicableOn));
					discount.DiscountApplicable = discountPerItemGroup;
					disAmount += discountPerItemGroup;
				}
				else { }
			}

			return disAmount;
		}

		#endregion

		#region Basket Management

		public static void AddProductToProductList(string name, decimal price)
		{
			var newProduct = Product.CreateNewProduct(name, price);
			ShoppingBasket.availableProducts.Add(newProduct);
		}

		public static void AddProductToBasket(int IDproduct)
		{
			ShoppingBasket.basketedProducts.Add(ShoppingBasket.availableProducts[IDproduct - 1]);
		}

		public static List<Product> GetAvailableProducts()
		{
			return ShoppingBasket.availableProducts;
		}

		public static List<Product> GetBasketedProducts()
		{
			return ShoppingBasket.basketedProducts;
		}

		#endregion

		#region Discount Management

		public static void AddDiscount(Product sourceProduct, int requiredAmount, Product targetProduct, decimal discount)
		{
			var newDiscount = Discount.CreateNewDiscount(sourceProduct, requiredAmount, targetProduct, discount);
			ShoppingBasket.discounts.Add(newDiscount);
		}

		public static List<Discount> GetDiscounts()
		{
			return ShoppingBasket.discounts;
		}

		#endregion
	}
}
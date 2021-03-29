using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingBasketUnitTest
{
	[TestClass]
	public class UnitTest
	{

		private static void InitialiseData()
		{
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Bread", 1);
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Milk", 1.15m);
			ShoppingBasket.ShoppingBasket.AddProductToProductList("Butter", 0.80m);

			ShoppingBasket.ShoppingBasket.AddDiscount(ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Butter"), 2,
																	ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Bread"), 50);


			ShoppingBasket.ShoppingBasket.AddDiscount(ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Milk"), 4,
																	ShoppingBasket.ShoppingBasket.GetAvailableProducts().First(x => x.Name == "Milk"), 100);
		}

		[TestMethod]
		public void TestBasketSum_ex1()
		{

			InitialiseData();


			ShoppingBasket.ShoppingBasket.AddProductToBasket(1);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(3);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);

			decimal result = ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount();
			Assert.AreEqual(2.95m, result);
		}

		[TestMethod]
		public void TestBasketSum_ex2()
		{
			InitialiseData();
			
			ShoppingBasket.ShoppingBasket.AddProductToBasket(3);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(3);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(1);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(1);

			decimal result = ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount();
			Assert.AreEqual(3.10m, result);
		}

		[TestMethod]
		public void TestBasketSum_ex3()
		{
			InitialiseData();
		

			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);

			decimal result = ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount();
			Assert.AreEqual(3.45m, result);
		}

		[TestMethod]
		public void TestBasketSum_ex4()
		{
			InitialiseData();


			ShoppingBasket.ShoppingBasket.AddProductToBasket(3);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(3);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(1);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);
			ShoppingBasket.ShoppingBasket.AddProductToBasket(2);


			decimal result = ShoppingBasket.ShoppingBasket.CalculateShoppingBasketAmountWithDiscount();
			Assert.AreEqual(9m, result);
		}
	}
}

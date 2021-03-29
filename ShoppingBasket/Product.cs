namespace ShoppingBasket
{
	public class Product
	{
		private string name { get; set; }
		private decimal price { get; set; }

		public string Name
		{
			get => name;
			set => name = value;
		}
		public decimal Price
		{
			get => price;
			set => price = value;
		}
		public static Product CreateNewProduct(string name, decimal price)
		{
			return new Product
					{
						Name = name,
						Price = price,
					};
		}

	}

	public class Discount
	{
		private Product sourceProduct { get; set; }
		private int requiredAmmount { get; set; }
		private Product targetProduct { get; set; }
		private decimal discountDefined { get; set; }
		private decimal discountApplicable { get; set; }

		public Product SourceProduct
		{
			get => sourceProduct;
			set => sourceProduct = value;
		}
		public int RequiredAmmount
		{
			get => requiredAmmount;
			set => requiredAmmount = value;
		}
		public Product TargetProduct
		{
			get => targetProduct;
			set => targetProduct = value;
		}
		public decimal DiscountDefined
		{
			get => discountDefined;
			set => discountDefined = value;
		}
		public decimal DiscountApplicable
		{
			get => discountApplicable;
			set => discountApplicable = value;
		}

		public static  Discount CreateNewDiscount(Product sourceProduct, int requiredAmount, Product targetProduct, decimal discount)
		{
			return new Discount
					{
						SourceProduct = targetProduct,
						RequiredAmmount = requiredAmount,
						TargetProduct = sourceProduct,
						DiscountDefined = discount
					};
		}
	}
}
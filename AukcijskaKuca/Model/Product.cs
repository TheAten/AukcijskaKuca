namespace AukcijskaKuca.Model
{
	public class Product
	{

		public int Id
		{
			get;
			set;
		}

		private float lastOffer;

		public float LastOffer
		{
			get { return lastOffer; }
			set { lastOffer = value; }
		}

		private float price;

		public float Price
		{
			get { return price; }
			set { price = value; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

	}
}

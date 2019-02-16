namespace Ordering.Service.Model
{
    public class Line
    {
        private Line()
        {
        }

        public static Line New(string productName, int quantity)
        {
            return new Line
            {
                ProductName = productName,
                Quantity = quantity
            };
        }

        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
    }
}
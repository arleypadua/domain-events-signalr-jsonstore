namespace Ordering.Service.Model
{
    public class Line
    {
        public Line(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
    }
}
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(int productId, string name, double price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }
}
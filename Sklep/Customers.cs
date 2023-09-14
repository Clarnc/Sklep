using Sklep;

public class Customer
{
    public int CustomerId { get; private set; }
    public string Name { get; private set; }
    private Cart Cart { get; set; }

    public Customer(int customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
        Cart = new Cart(this);
    }

    public void AddToCart(Product product)
    {
        Cart.AddToCart(product);
        SaveCartItems();
    }

    public void ViewCart()
    {
        Cart.ViewCart();
    }

    public double CalculateTotal()
    {
        return Cart.CalculateTotal();
    }

    public List<Product> GetCartContents()
    {
        return Cart.GetCartContents();
    }
    public void LoadCartItems()
    {
        Cart.LoadCartItems();
    }

    public void PlaceOrder()
    {
        double total = CalculateTotal();

        if (total > 0)
        {
            // Implement order placement logic here (e.g., sending an order to a server or updating a database).
            // For this example, we'll just display a message.

            Console.WriteLine($"Order placed for {Name} with a total value of {total:C}");

        }
        else
        {
            Console.WriteLine("Cart is empty. Cannot place an order.");
        }
    }
    private void SaveCartItems()
    {
        Cart.SaveCartItems();
    }

}
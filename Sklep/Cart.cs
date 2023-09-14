using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    
    public class Cart 
    {
        private List<Product> products;
        public Customer Customer { get; private set; }

        public Cart(Customer customer)
        {
            Customer = customer;
            products = new List<Product>();
            
    }

        public void AddToCart(Product product)
        {
            products.Add(product);
        }

        public void ViewCart()
        {
            Console.WriteLine("Cart Contents:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var product in products)
            {
                total += product.Price;
            }
            return total;
        }

        public List<Product> GetCartContents()
        {
            return products;
        }
        public void LoadCartItems()
        {
            string cartFileName = $"{Customer.CustomerId}_cart.json";
            if (File.Exists(cartFileName))
            {
                string json = File.ReadAllText(cartFileName);
                List<Product> cartItems = JsonConvert.DeserializeObject<List<Product>>(json);
                products = cartItems;
                Console.WriteLine("Cart items loaded from JSON file.");
            }
        }

        public void SaveCartItems()
        {
            string json = JsonConvert.SerializeObject(products);
            string cartFileName = $"{Customer.CustomerId}_cart.json"; // Use a unique file name for each customer's cart
            File.WriteAllText(cartFileName, json);
            Console.WriteLine("Cart items saved to JSON file.");
        }
    }
}
    


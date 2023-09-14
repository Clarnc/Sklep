using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    class Program
    {
        private static List<Customer> customers = new List<Customer>();
        private static List<Product> products = new List<Product>();
        private static Customer selectedCustomer;
        private const string customersJsonFile = "customers.json";
        private const string productsJsonFile = "products.json";

        // Load customer data from JSON file
        // Display the "View Products" screen
        private static void ViewProductsScreen()
        {
            Console.Clear();
            Console.WriteLine("View Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
            }
            Console.WriteLine("Press 'R' to return to the main menu.");
            while (Console.ReadKey(true).Key != ConsoleKey.R) { }
        }

        // Display the "View Cart" screen
        private static void ViewCartScreen()
        {
            Console.Clear();
            Console.WriteLine("View Cart Contents:");
            List<Product> cartContents = selectedCustomer.GetCartContents();
           
            if (cartContents.Count > 0)
            {
                foreach (var product in cartContents)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
                }
            }
            else
            {
                Console.WriteLine("Cart is empty.");
            }
            Console.WriteLine("Press 'R' to return to the main menu.");
            while (Console.ReadKey(true).Key != ConsoleKey.R) { }
        }
        private static void LoadCustomers()
        {
            if (File.Exists(customersJsonFile))
            {
                string json = File.ReadAllText(customersJsonFile);
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }

        // Save customer data to JSON file
        private static void SaveCustomers()
        {
            string json = JsonConvert.SerializeObject(customers);
            File.WriteAllText(customersJsonFile, json);
        }

        // Load product data from JSON file
        private static void LoadProducts()
        {
            if (File.Exists(productsJsonFile))
            {
                string json = File.ReadAllText(productsJsonFile);
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }
        }

        // Save product data to JSON file
      

        // Display a list of products
        private static void DisplayProducts()
        {
            Console.WriteLine("\nList of Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
            }
        }

        // Select a product by ID
        private static Product SelectProduct()
        {
            Console.Write("\nEnter Product ID to add to the cart: ");
            int selectedProductId = Convert.ToInt32(Console.ReadLine());

            Product selectedProduct = products.Find(p => p.ProductId == selectedProductId);
            if (selectedProduct != null)
            {
                return selectedProduct;
            }
            else
            {
                Console.WriteLine("Product not found.");
                return null;
            }
        }

        // Add a product to the cart
        private static void AddToCart(Customer customer, Product product)
        {
            customer.AddToCart(product);
            Console.WriteLine($"{product.Name} added to {customer.Name}'s cart successfully!");
        }

        // Display the cart contents
        private static void DisplayCart(Customer customer)
        {
            Console.WriteLine($"Cart Contents for {customer.Name}:");
            foreach (var product in customer.GetCartContents())
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
            }
        }

        static void Main(string[] args)
        {
            LoadCustomers(); // Load existing customer data (if any)
            LoadProducts(); // Load existing product data (if any)
            bool loggedIn = false;
            while (true)
            {
              

                if (!loggedIn)
                {
                    Console.WriteLine("\nOptions:");
                    Console.WriteLine("1. Register Customer");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                    Console.Write("Select an option: ");

                    string preLoginChoice = Console.ReadLine();

                    switch (preLoginChoice)
                    {
                        case "1":
                            Console.WriteLine("Customer Registration:");
                            Console.Write("Enter Customer Name: ");
                            string customerName = Console.ReadLine();
                            int customerId = customers.Count + 1;
                            Customer newCustomer = new Customer(customerId, customerName);
                            customers.Add(newCustomer);
                            SaveCustomers(); // Save customer data to JSON file
                            Console.Clear();
                           
                            break;
                        case "2":
                            Console.WriteLine("\nSelect Customer:");
                            foreach (var customer in customers)
                            {
                                Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.Name}");
                            }
                            Console.Write("Enter Customer ID: ");
                            int selectedCustomerId = Convert.ToInt32(Console.ReadLine());
                            selectedCustomer = customers.Find(c => c.CustomerId == selectedCustomerId);
                            if (selectedCustomer != null)
                            {
                                Console.WriteLine($"Selected Customer: {selectedCustomer.Name}");
                                loggedIn = true;
                                selectedCustomer.LoadCartItems();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Customer not found.");
                            }
                            break;
                           
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                else // Logged in
                {
                  
                  
                    Console.WriteLine("\nLogged in as: " + selectedCustomer.Name);
                    Console.WriteLine("Options:");
                    Console.WriteLine("1. View Products");
                    Console.WriteLine("2. Add Product to Cart");
                    Console.WriteLine("3. View Cart");
                    Console.WriteLine("4. Place Order");
                    Console.WriteLine("5. Logout");
                    Console.WriteLine("6. Exit");
                    Console.Write("Select an option: ");

                    string loggedInChoice = Console.ReadLine();

                    switch (loggedInChoice)
                    {
                      
                        case "1":
                            
                            ViewProductsScreen();
                            
                            break;
                        case "2":
                           
                                DisplayProducts();
                            
                            Product selectedProduct = SelectProduct();
                                if (selectedProduct != null)
                                {
                                    AddToCart(selectedCustomer, selectedProduct);
                                }
                           
                            break;
                        case "3":
                            ViewCartScreen();
                            break;
                        case "4":
                            
                                Console.WriteLine($"Placing an order for {selectedCustomer.Name} with the following items:");
                                selectedCustomer.LoadCartItems();
                                DisplayCart(selectedCustomer);
                                selectedCustomer.PlaceOrder();
                                
                               
                            
                          
                            break;
                        case "5":
                            loggedIn = false; // Logout
                            selectedCustomer = null; // Clear selected customer
                            break;
                            
                        case "6":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
            }
        }
    }
}


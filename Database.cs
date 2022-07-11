using Bogus;
using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;

namespace LetsMarket
{
    public enum DatabaseOption { Employees, Products, Clients }

    public class Database
    {
        protected static readonly string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        protected static readonly string employeesDb = Path.Combine(rootDirectory, "employees.xml");
        protected static readonly string productsDb = Path.Combine(rootDirectory, "products.xml");
        protected static readonly string clientsDb = Path.Combine(rootDirectory, "clients.xml");

        public static List<Employee> Employees = new List<Employee>();
        public static List<Product> Products = new List<Product>();
        public static List<Client> Clients = new List<Client>();

        static Database()
        {
            DatabaseBaseValidation.ValidateDatabase();
            Load.LoadEmployee();
            Load.LoadProduct();
            Load.LoadClient();
        }

        public static void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }
        public static void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }
        public static void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
        public static void RemoveClient(Client client)
        {
            Clients.Remove(client);
        }
        public static void AddClient(Client client)
        {
            Clients.Add(client);
        }
    }
}
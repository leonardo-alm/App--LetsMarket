using System.Xml.Serialization;

namespace LetsMarket.validations
{
    public class Load : Database
    {

        public static void LoadEmployee()
        {

            XmlSerializer employeeSerializer = new XmlSerializer(typeof(List<Employee>));
            using (TextReader reader = new StreamReader(Database.employeesDb))
            {
                var employees = employeeSerializer.Deserialize(reader) as List<Employee>;
                Employees = employees ?? new List<Employee>();
            }
        }

        public static void LoadProduct()
        {
            XmlSerializer productSerializer = new XmlSerializer(typeof(List<Product>));
            using (TextReader reader = new StreamReader(Database.productsDb))
            {
                var products = productSerializer.Deserialize(reader) as List<Product>;
                Products = products ?? new List<Product>();
            }
        }

        public static void LoadClient()
        {
            XmlSerializer clientSerializer = new XmlSerializer(typeof(List<Client>));
            using (TextReader reader = new StreamReader(Database.clientsDb))
            {
                var clients = clientSerializer.Deserialize(reader) as List<Client>;
                Clients = clients ?? new List<Client>();
            }

        }
    }
}
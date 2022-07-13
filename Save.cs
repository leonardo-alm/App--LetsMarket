using System.Xml.Serialization;

namespace LetsMarket.validations
{
    public class Save : Database
    {
        public static void SaveEmployee()
        {
            Console.WriteLine("Salvando...");

            XmlSerializer employeeSerializer = new XmlSerializer(typeof(List<Employee>));
            using (TextWriter writer = new StreamWriter(employeesDb))
            {
                employeeSerializer.Serialize(writer, Employees);
            }
            Console.WriteLine("Salvo.");
        }

        public static void SaveProduct()
        {
            XmlSerializer productSerializer = new XmlSerializer(typeof(List<Product>));
            using (TextWriter writer = new StreamWriter(productsDb))
            {
                productSerializer.Serialize(writer, Products);
            }
            Console.WriteLine("Salvo.");
        }
        public static void SaveClient()
        {
            XmlSerializer clientSerializer = new XmlSerializer(typeof(List<Client>));
            using (TextWriter writer = new StreamWriter(clientsDb))
            {
                clientSerializer.Serialize(writer, Clients);
            }
        }

    }

}
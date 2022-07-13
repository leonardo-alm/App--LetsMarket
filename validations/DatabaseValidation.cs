using CsvHelper;
using System.Globalization;

namespace LetsMarket.validations
{
    public class DatabaseValidation : Database
    {

        public static void ValidateDatabase()
        {
            if (!File.Exists(employeesDb))
            {
                Employees.Add(new Employee { Name = "Admin", Login = "admin", Password = "admin" });
                Save.SaveEmployee();
            }

            if (!File.Exists(productsDb) && File.Exists("dados.csv"))
            {
                var faker = new Bogus.DataSets.Commerce();

                using (var reader = new StreamReader("dados.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvReaderClassMap>();
                    var products = csv.GetRecords<Product>().ToList();
                    Products = products.OrderBy(x => Guid.NewGuid()).Take(10).ToList();
                    products.ForEach(x => x.Price = decimal.Parse(faker.Price()));

                    Save.SaveProduct();
                }
            }

            if (!File.Exists(clientsDb))
            {
                for (int i = 0; i < 10; i++)
                    Clients.Add(ClienteFaker.Gerar());

                Save.SaveClient();
            }

        }
    }
}
using BetterConsoleTables;
using Sharprompt;

namespace LetsMarket
{
    public class Sale
    {

        private static int _largestColumn;
        private string _description;
        public static void SetSize(int size) => _largestColumn = size;
        public string ProductCode { get; set; }
        public string Description
        {
            get => _description;
            set
            {
                _description = value.PadRight(_largestColumn + 5);
            }
        }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get => Amount * UnitPrice; }

        public override string ToString()
        {
            return Description;
        }


        public static void MakeSale()
        {
            var total = decimal.Zero;
            var max = Database.Products.Max(x => x.Description.Length);
            Sale.SetSize(max);

            var saleItems = new List<Sale>();

            var cpf = Prompt.Input<string>("Digite o documento para identificar o cliente ou [ENTER] para continuar");
            if (!string.IsNullOrEmpty(cpf))
            {
                var clientName = "";
                foreach (var client in Database.Clients)
                {
                    if (client.Cpf == cpf)
                        clientName = client.Name;
                }
                if (!string.IsNullOrEmpty(clientName))
                    Console.WriteLine($"{clientName}");
            }

            var products = Database.Products.ToList();
            var exit = new Product { ProductCode = "-1", Description = "Sair", Price = 0 };
            var closeSale = new Product { ProductCode = "-1", Description = "Fechar Venda", Price = 0 };
            var cancelItem = new Product { ProductCode = "-1", Description = "Cancelar Item", Price = 0 };

            products.Add(cancelItem);
            products.Add(closeSale);
            products.Add(exit);

            Product product = null;
            do
            {
                Console.Clear();
                Console.WriteLine("EFETUANDO UMA VENDA");

                var report = new Table(TableConfiguration.UnicodeAlt());
                var largestColumn = Database.Products.Max(x => x.Description);

                if (saleItems.Count > 0)
                {
                    report.From<Sale>(saleItems);
                    Console.WriteLine(report.ToString());
                }

                Console.WriteLine();
                Console.WriteLine();

                // Early Return
                product = Prompt.Select("Selecione o produto", products);
                if (product != exit && product != closeSale && product != cancelItem)
                {
                    var amount = Prompt.Input<int>("Informe a quantidade", defaultValue: 1);
                    var item = new Sale
                    {
                        ProductCode = product.ProductCode,
                        Description = product.Description,
                        UnitPrice = product.Price,
                        Amount = amount
                    };
                    saleItems.Add(item);
                    total += item.Subtotal;
                }

                if (product == cancelItem)
                {
                    Console.Clear();
                    Console.WriteLine("Selecione o item a ser cancelado");
                    var item = Prompt.Select("Selecione o item a ser cancelado", saleItems);
                    saleItems.Remove(item);

                    total -= item.UnitPrice;
                }
            } while (product != exit && product != closeSale);

            if (product == closeSale)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"TOTAL DA COMPRA: {total}");
                Console.ForegroundColor = color;
                Console.ReadKey();
            }

            products.Remove(exit);
            products.Remove(closeSale);
            products.Remove(cancelItem);

            return;
        }
    }
}
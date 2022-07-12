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

            CpfValidation.ValidateCpf();

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

               
                product = Prompt.Select("Selecione o produto", products);
                
                                              
                total = ProductValidation.ValidateProduct(exit, closeSale, cancelItem, products, saleItems, total, product);
               
                if (product == cancelItem && saleItems.Count > 0)
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
                Console.ForegroundColor = ConsoleColor.Green;
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
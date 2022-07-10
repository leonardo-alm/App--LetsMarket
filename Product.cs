using BetterConsoleTables;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace LetsMarket
{
    public class Product
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código é obrigatório")]
        public string ProductCode { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Price { get; set; }

        public static void RegisterProduct()
        {
            var product = Prompt.Bind<Product>();

            if (!Prompt.Confirm("Deseja Salvar?"))
                return;

            Database.Products.Add(product);
            Database.Save(DatabaseOption.Products);
        }

        public static void ListProducts()
        {
            Console.WriteLine("Listando Produtos");
            Console.WriteLine();

            var table = new Table(TableConfiguration.UnicodeAlt());
            table.From(Database.Products);
            Console.WriteLine(table.ToString());
        }

        public override string ToString()
        {
            return Description;
        }

        public static void EditProduct()
        {
            var product = Prompt.Select("Selecione o Produto para Editar", Database.Products, defaultValue: Database.Products[0]);

            Prompt.Bind(product);

            Database.Save(DatabaseOption.Products);
        }

        public static void RemoveProduct()
        {
            var product = Prompt.Select("Selecione o Produto para Remover", Database.Products);
            var confirm = Prompt.Confirm("Tem Certeza?", false);

            if (!confirm)
                return;

            Database.Products.Remove(product);
            Database.Save(DatabaseOption.Products);
        }
    }
}
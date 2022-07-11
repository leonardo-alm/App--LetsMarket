using BetterConsoleTables;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace LetsMarket
{
    public class Client
    {
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "O Documento é Obrigatório")]
        [MinLength(11)]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Display(Name = "Categoria")]
        public ClientCategory? Category { get; set; }

        public static void RegisterClient()
        {
            var employee = Prompt.Bind<Client>();

            if (!Prompt.Confirm("Deseja Salvar?"))
                return;

            Database.AddClient(employee);
            Save.SaveClient();
        }
        public static void ListClients()
        {
            Console.WriteLine("Listando Clientes");
            Console.WriteLine();

            var table = new Table(TableConfiguration.UnicodeAlt());
            table.From(Database.Clients);
            Console.WriteLine(table.ToString());
        }

        public override string ToString()
        {
            return $"{Name} - {Cpf}";
        }

        public static void EditClient()
        {
            var client = Prompt.Select("Selecione o Cliente para Editar", Database.Clients, defaultValue: Database.Clients[0]);

            Prompt.Bind(client);

            Save.SaveClient();
        }

        public static void RemoveClient()
        {
            if (Database.Clients.Count == 1)
            {
                ConsoleInput.WriteError("Não é possível remover todos os usuários.");
                Console.ReadKey();
                return;
            }

            var client = Prompt.Select("Selecione o Cliente para Remover", Database.Clients);
            var confirm = Prompt.Confirm("Tem Certeza?", false);

            if (!confirm)
                return;

            Database.RemoveClient(client);
            Save.SaveClient();
        }
    }
}
using BetterConsoleTables;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace LetsMarket.validations
{
    public class Employee
    {
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Categoria")]
        public EmployeeCategory Category { get; set; }

        public static void RegisterEmployee()
        {
            var employee = Prompt.Bind<Employee>();
            if (!Prompt.Confirm("Deseja Salvar?"))
                return;
            
            Database.AddEmployee(employee);            

            if (Database.Employees.Count > 1 && Database.Employees[0].Login == "admin" && Database.Employees[0].Password == "admin")
            {
                Database.RemoveEmployee(Database.Employees[0]);
                
            }
            Save.SaveEmployee();
        }
        #region Unused Code
        //private static string CreateLoginSuggestionBasedOnName(string name)
        //{
        //    var parts = name?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        //    var suggestion = parts?.Length > 0 ? (parts.Length > 1 ? $"{parts[0]}.{parts[parts.Length - 1]}" : $"{parts[0]}") : "";

        //    return suggestion.ToLower();
        //}
        #endregion
        public static void ListEmployees()
        {
            Console.WriteLine("Listando Funcionários");
            Console.WriteLine();

            var table = new Table(TableConfiguration.UnicodeAlt());
            table.From(Database.Employees);
            Console.WriteLine(table.ToString());
        }

        public override string ToString()
        {
            return Name;
        }

        public static void EditEmployee()
        {
            var employee = Prompt.Select("Selecione o Funcionário para Editar", Database.Employees, defaultValue: Database.Employees[0]);

            Prompt.Bind(employee);

            Save.SaveEmployee();
        }

        public static void RemoveEmployee()
        {
            if (Database.Employees.Count == 1)
            {
                ConsoleInput.WriteError("Não é possível remover todos os usuários.");
                Console.ReadKey();
                return;
            }

            var employee = Prompt.Select("Selecione o Funcionário para Remover", Database.Employees);
            var confirm = Prompt.Confirm("Tem Certeza?", false);

            if (!confirm)
                return;

            Database.RemoveEmployee(employee);
            Save.SaveEmployee();
        }
    }
}
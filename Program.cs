using Sharprompt;

namespace LetsMarket
{
    public class Program
    {
        static void Main()
        {
            MenuItem.SetPrompt();
            Console.Title = "Let's Store";

            Login.VerifyLogin();

            var menu = new MenuItem("Menu Principal");

            var products = new MenuItem("Produtos");
            products.Add(new MenuItem("Cadastrar Produto", Product.RegisterProduct));
            products.Add(new MenuItem("Listar Produtos", Product.ListProducts));
            products.Add(new MenuItem("Editar Produto", Product.EditProduct));
            products.Add(new MenuItem("Remover Produto", Product.RemoveProduct));

            var employees = new MenuItem("Funcionários");
            employees.Add(new MenuItem("Cadastrar Funcionário", Employee.RegisterEmployee));
            employees.Add(new MenuItem("Listar Funcionários", Employee.ListEmployees));
            employees.Add(new MenuItem("Editar Funcionário", Employee.EditEmployee));
            employees.Add(new MenuItem("Remover Funcionário", Employee.RemoveEmployee));

            var clients = new MenuItem("Clientes");
            clients.Add(new MenuItem("Cadastrar Cliente", Client.RegisterClient));
            clients.Add(new MenuItem("Listar Clientes", Client.ListClients));
            clients.Add(new MenuItem("Editar Cliente", Client.EditClient));
            clients.Add(new MenuItem("Remover Cliente", Client.RemoveClient));

            var sales = new MenuItem("Vendas");
            sales.Add(new MenuItem("Efetuar Venda", Sale.MakeSale));

            menu.Add(products);
            menu.Add(employees);
            menu.Add(clients);
            menu.Add(sales);
            menu.Add(new MenuItem("Sair", () => Environment.Exit(0)));

            menu.Execute();
            
        }
    }
}
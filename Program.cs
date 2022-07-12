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

            MenuInitialization.InitializeMenu();            
            
        }
    }
}
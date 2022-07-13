using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharprompt;

namespace LetsMarket.validations
{
    public class CpfValidation
    {
        public static void ValidateCpf()
        {
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
        }
     
    }
}

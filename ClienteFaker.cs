using Bogus;
using Bogus.Extensions.Brazil;

namespace LetsMarket
{
    public static class ClienteFaker
    {
        public static Faker<Client> Gerar()
        {
            Faker<Client> client = new Faker<Client>("pt_BR")
                .RuleFor(s => s.Name, f => f.Person.FullName)
                .RuleFor(s => s.Cpf, f => f.Person.Cpf())
                .RuleFor(s => s.Category, f => f.PickRandom<ClientCategory>());

            return client;
        }
    }
}
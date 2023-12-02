
class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public int CalcularIdade()
    {
        DateTime hoje = DateTime.Now;
        int idade = hoje.Year - DataNascimento.Year - ((hoje.Month > DataNascimento.Month || (hoje.Month == DataNascimento.Month && hoje.Day >= DataNascimento.Day)) ? 0 : 1);
        return idade;
    }
}

class Treinador : Pessoa
{
    public string CREF { get; set; }
}

class Cliente : Pessoa
{
    public double Altura { get; set; }
    public double Peso { get; set; }

    public double CalcularIMC()
    {
        return Peso / (Altura * Altura);
    }
}

class Program
{
    static void Main()
    {
        List<Treinador> treinadores = new List<Treinador>();
        List<Cliente> clientes = new List<Cliente>();

        try
        {
            Treinador treinador1 = new Treinador { Nome = "Treinador1", DataNascimento = new DateTime(1980, 5, 15), CPF = "12345678901", CREF = "CREF123" };
            Treinador treinador2 = new Treinador { Nome = "Treinador2", DataNascimento = new DateTime(1990, 10, 20), CPF = "98765432101", CREF = "CREF456" };
            treinadores.Add(treinador1);
            treinadores.Add(treinador2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


        try
        {
            Cliente cliente1 = new Cliente { Nome = "Cliente1", DataNascimento = new DateTime(1995, 3, 10), CPF = "11122233344", Altura = 1.75, Peso = 75 };
            Cliente cliente2 = new Cliente { Nome = "Cliente2", DataNascimento = new DateTime(1988, 12, 5), CPF = "55566677788", Altura = 1.60, Peso = 65 };
            clientes.Add(cliente1);
            clientes.Add(cliente2);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }



        Console.WriteLine("Treinadores com idade entre 25 e 40 anos:");
        foreach (var treinador in RelatorioTreinadoresIdade(treinadores, 25, 40))
        {
            Console.WriteLine($"{treinador.Nome}, {treinador.CalcularIdade()} anos");
        }

        Console.WriteLine("\nClientes com idade entre 20 e 35 anos:");
        foreach (var cliente in RelatorioClientesIdade(clientes, 20, 35))
        {
            Console.WriteLine($"{cliente.Nome}, {cliente.CalcularIdade()} anos");
        }

        Console.WriteLine("\nClientes com IMC maior que 25 em ordem crescente:");
        foreach (var cliente in RelatorioClientesIMC(clientes, 25))
        {
            Console.WriteLine($"{cliente.Nome}, IMC: {cliente.CalcularIMC()}");
        }

        Console.WriteLine("\nClientes em ordem alfabética:");
        foreach (var cliente in RelatorioClientesOrdemAlfabetica(clientes))
        {
            Console.WriteLine($"{cliente.Nome}");
        }

        Console.WriteLine("\nClientes do mais velho para o mais novo:");
        foreach (var cliente in RelatorioClientesOrdemIdade(clientes))
        {
            Console.WriteLine($"{cliente.Nome}, {cliente.CalcularIdade()} anos");
        }
    }

    static List<Treinador> RelatorioTreinadoresIdade(List<Treinador> treinadores, int minIdade, int maxIdade)
    {
        return treinadores.Where(t => t.CalcularIdade() >= minIdade && t.CalcularIdade() <= maxIdade).ToList();
    }

    static List<Cliente> RelatorioClientesIdade(List<Cliente> clientes, int minIdade, int maxIdade)
    {
        return clientes.Where(c => c.CalcularIdade() >= minIdade && c.CalcularIdade() <= maxIdade).ToList();
    }

    static List<Cliente> RelatorioClientesIMC(List<Cliente> clientes, double minIMC)
    {
        return clientes.Where(c => c.CalcularIMC() > minIMC).OrderBy(c => c.CalcularIMC()).ToList();
    }

    static List<Cliente> RelatorioClientesOrdemAlfabetica(List<Cliente> clientes)
    {
        return clientes.OrderBy(c => c.Nome).ToList();
    }

    static List<Cliente> RelatorioClientesOrdemIdade(List<Cliente> clientes)
    {
        return clientes.OrderByDescending(c => c.CalcularIdade()).ToList();
    }
}

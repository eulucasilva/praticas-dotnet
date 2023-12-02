using System.Text.RegularExpressions;
class Pessoa
{

    private string? _nome;
    private DateTime _nascimento;
    private string? _cpf;
    public string? Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("Nome não pode ser nulo");
            _nome = value;
        }
    }

    public DateTime Nascimento
    {
        get => _nascimento;
        set
        {
            if (value > DateTime.Now) throw new Exception("Data de nascimento inválida");
            _nascimento = value;
        }
    }

    public string? Cpf
    {
        get => _cpf;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 11) throw new Exception("Cpf não pode ser nulo");
            _cpf = value;
        }
    }

    public virtual void ExibirDetalhes()
    {
        Console.WriteLine($"Nome: {Nome}, Idade: {CalcularIdade()} anos");
    }

    public int CalcularIdade()
    {
        DateTime hoje = DateTime.Now;
        int idade = hoje.Year - Nascimento.Year - ((hoje.Month > Nascimento.Month || (hoje.Month == Nascimento.Month && hoje.Day >= Nascimento.Day)) ? 0 : 1);
        return idade;
    }
}

class Treinador : Pessoa
{
     private string? _cref;

    // Propriedades
    public string? Cref
    {
        get => _cref;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("Cref não pode ser nulo");
            _cref = value;
        }
    }

    public override void ExibirDetalhes()
    {
        base.ExibirDetalhes();
        Console.WriteLine($"CREF: {Cref}");
    }
}

class Cliente : Pessoa
{
    private float _altura;
    private float _peso;
    
    // Propriedades
    public float Altura
    {
        get => _altura;
        set
        {
            if (value < 0) throw new Exception("Altura não pode ser negativa");
            _altura = value;
        }
    }
    public float Peso
    {
        get => _peso;
        set
        {
            if (value < 0) throw new Exception("Peso não pode ser negativo");
            _peso = value;
        }
    }

    public double CalcularIMC()
    {
        return Peso / (Altura * Altura);
    }

    public static bool validaData(string data)
    {
        const string padrao = @"\d{2}/\d{2}/\d{4}";
        if (Regex.IsMatch(data, padrao)) return true;
        return false;
    }

    public static bool validaCpf(string cpf)
    {
        if (Regex.IsMatch(cpf, @"^\d+$") && cpf.Length == 11)
        {
            return true;
        }

        return false;
    }

    public static bool validaCref(string cref)
    {
        const string padrao = @"\d{6}-\d{1}/[A-Z]{2}";
        if (Regex.IsMatch(cref, padrao))
        {
            return true;
        }

        return false;
    }

    public static int calculaIdade(DateTime dataNascimento)
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - dataNascimento.Year;
        if (dataNascimento > hoje.AddYears(-idade))
            idade--;
        return idade;
    }
}

class Program
{
    static List<Pessoa> pessoas = new List<Pessoa>();
    static void Main()
    {

        AdicionarPessoa(new Treinador { Nome = "João Treinador", Nascimento = new DateTime(1980, 5, 15), Cpf = "12345678901", Cref = "CREF123" });
        AdicionarPessoa(new Treinador { Nome = "Maria Treinadora", Nascimento = new DateTime(1990, 10, 20), Cpf = "98765432101", Cref = "CREF456" });

        AdicionarPessoa(new Cliente { Nome = "Carlos Cliente", Nascimento = new DateTime(1995, 3, 10), Cpf = "11122233344", Altura = 1.75F, Peso = 75 });
        AdicionarPessoa(new Cliente { Nome = "Ana Cliente", Nascimento = new DateTime(1988, 12, 5), Cpf = "55566677788", Altura = 1.60F, Peso = 65 });

        Console.WriteLine("Lista de Pessoas:");
        ListarPessoas();

        Console.WriteLine("\nAtualizar Pessoa:");
        AtualizarPessoa("11122233344", new Cliente { Nome = "Carlos Cliente Atualizado", Nascimento = new DateTime(1995, 3, 10), Cpf = "11122233344", Altura = 1.80F, Peso = 80 });
        ListarPessoas();

        Console.WriteLine("\nRemover Pessoa:");
        RemoverPessoa("CREF123");
        ListarPessoas();

        Console.WriteLine("\nRelatório: Treinadores com idade entre 25 e 40 anos");
        RelatorioTreinadoresIdade(25, 40);

        Console.WriteLine("\nRelatório: Clientes com idade entre 20 e 35 anos");
        RelatorioClientesIdade(20, 35);

        Console.WriteLine("\nRelatório: Clientes com IMC maior que 25 em ordem crescente");
        RelatorioClientesIMC(25);

        Console.WriteLine("\nRelatório: Clientes em ordem alfabética");
        RelatorioClientesOrdemAlfabetica();

        Console.WriteLine("\nRelatório: Clientes do mais velho para o mais novo");
        RelatorioClientesOrdemIdade();

        Console.WriteLine("\nRelatório: Aniversariante do mês");
        RelatorioAniversariantesDoMes();


    }

    static DateTime ValidarData(string dataStr)
    {
        DateTime data;
        while (!DateTime.TryParse(dataStr, out data) || data > DateTime.Now)
        {
            Console.Write("Data inválida ou no futuro. Digite novamente (YYYY-MM-DD): ");
            dataStr = Console.ReadLine();
        }
        return data;
    }

    static string ValidarCPF(string cpf)
    {
        while (!IsValidCPF(cpf))
        {
            Console.Write("CPF inválido. Digite novamente (XXX.XXX.XXX-XX): ");
            cpf = Console.ReadLine();
        }
        return cpf;
    }

    static bool IsValidCPF(string cpf)
    {
        return cpf.Length == 14 && cpf.Count(char.IsDigit) == 11;
    }

    static void RelatorioTreinadoresIdade(int minIdade, int maxIdade)
    {
        var treinadores = pessoas.OfType<Treinador>().Where(t => t.CalcularIdade() >= minIdade && t.CalcularIdade() <= maxIdade).ToList();
        foreach (var treinador in treinadores)
        {
            treinador.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void RelatorioClientesIdade(int minIdade, int maxIdade)
    {
        var clientes = pessoas.OfType<Cliente>().Where(c => c.CalcularIdade() >= minIdade && c.CalcularIdade() <= maxIdade).ToList();
        foreach (var cliente in clientes)
        {
            cliente.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void RelatorioClientesIMC(double minIMC)
    {
        var clientes = pessoas.OfType<Cliente>().Where(c => c.CalcularIMC() > minIMC).OrderBy(c => c.CalcularIMC()).ToList();
        foreach (var cliente in clientes)
        {
            cliente.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void RelatorioClientesOrdemAlfabetica()
    {
        var clientes = pessoas.OfType<Cliente>().OrderBy(c => c.Nome).ToList();
        foreach (var cliente in clientes)
        {
            cliente.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void RelatorioClientesOrdemIdade()
    {
        var clientes = pessoas.OfType<Cliente>().OrderByDescending(c => c.CalcularIdade()).ToList();
        foreach (var cliente in clientes)
        {
            cliente.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void RelatorioAniversariantesDoMes()
    {
        int mesAtual = DateTime.Now.Month;
        var aniversariantes = pessoas.Where(p => p.Nascimento.Month == mesAtual).ToList();

        foreach (var pessoa in aniversariantes)
        {
            pessoa.ExibirDetalhes();
            Console.WriteLine();
        }
    }
    static void AdicionarPessoa(Pessoa pessoa)
    {
        pessoas.Add(pessoa);
    }

    static void ListarPessoas()
    {
        foreach (var pessoa in pessoas)
        {
            pessoa.ExibirDetalhes();
            Console.WriteLine();
        }
    }

    static void AtualizarPessoa(string cpf, Pessoa pessoaAtualizada)
    {
        Pessoa pessoaExistente = pessoas.FirstOrDefault(p => p.Cpf == cpf);
        if (pessoaExistente != null)
        {
            pessoaExistente.Nome = pessoaAtualizada.Nome;
            pessoaExistente.Nascimento = pessoaAtualizada.Nascimento;

            if (pessoaExistente is Cliente clienteExistente && pessoaAtualizada is Cliente clienteAtualizado)
            {
                clienteExistente.Altura = clienteAtualizado.Altura;
                clienteExistente.Peso = clienteAtualizado.Peso;
            }
        }
    }

    static void RemoverPessoa(string cref)
    {
        Pessoa pessoaExistente = pessoas.FirstOrDefault(p => p is Treinador && ((Treinador)p).Cref == cref);
        if (pessoaExistente != null)
        {
            pessoas.Remove(pessoaExistente);
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

class Program
{
    // Definição da tupla Produto
    //public record Produto(int Codigo, string Nome, int QuantidadeEmEstoque, double PrecoUnitario);

    static List<(int Codigo, string Nome, int QuantidadeEmEstoque, double PrecoUnitario)> estoque = new();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("=== Sistema de Gerenciamento de Estoque ===");
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Consultar Produto por Código");
            Console.WriteLine("3. Atualizar Estoque");
            Console.WriteLine("4. Relatórios");
            Console.WriteLine("5. Sair");

            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarProduto();
                    break;
                case "2":
                    ConsultarProduto();
                    break;
                case "3":
                    AtualizarEstoque();
                    break;
                case "4":
                    GerarRelatorios();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void CadastrarProduto()
    {
        try
        {
            Console.WriteLine("=== Cadastro de Produto ===");

            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Quantidade em Estoque: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.Write("Preço Unitário: ");
            double preco = double.Parse(Console.ReadLine());

            var novoProduto = (codigo, nome, quantidade, preco);
            estoque.Add(novoProduto);

            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Entrada inválida. Certifique-se de inserir dados válidos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void ConsultarProduto()
    {
        try
        {
            Console.WriteLine("=== Consulta de Produto ===");
            Console.Write("Digite o código do produto: ");
            int codigoConsulta = int.Parse(Console.ReadLine());

            var produtoEncontrado = estoque.FirstOrDefault(p => p.Codigo == codigoConsulta);

            if (produtoEncontrado.Nome==null)
            {
                throw new ProdutoNaoEncontradoException($"Produto com código {codigoConsulta} não encontrado.");
            }

            Console.WriteLine($"Nome: {produtoEncontrado.Nome}");
            Console.WriteLine($"Quantidade em Estoque: {produtoEncontrado.QuantidadeEmEstoque}");
            Console.WriteLine($"Preço Unitário: {produtoEncontrado.PrecoUnitario:C}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Entrada inválida. Certifique-se de inserir um código válido.");
        }
        catch (ProdutoNaoEncontradoException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void AtualizarEstoque()
    {
        try
        {
            Console.WriteLine("=== Atualização de Estoque ===");
            Console.Write("Digite o código do produto: ");
            int codigoAtualizacao = int.Parse(Console.ReadLine());

            var produtoAtualizacao = estoque.FirstOrDefault(p => p.Codigo == codigoAtualizacao);

            if (produtoAtualizacao.Equals(null))
            {
                throw new ProdutoNaoEncontradoException($"Produto com código {codigoAtualizacao} não encontrado.");
            }

            Console.Write("Informe a quantidade para adicionar (+) ou remover (-): ");
            int quantidadeAtualizacao = int.Parse(Console.ReadLine());

            if (produtoAtualizacao.QuantidadeEmEstoque + quantidadeAtualizacao < 0)
            {
                throw new EstoqueInsuficienteException("Quantidade insuficiente em estoque para a remoção desejada.");
            }

            produtoAtualizacao = produtoAtualizacao with { QuantidadeEmEstoque = produtoAtualizacao.QuantidadeEmEstoque + quantidadeAtualizacao };
            Console.WriteLine("Estoque atualizado com sucesso!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Entrada inválida. Certifique-se de inserir um código e uma quantidade válidos.");
        }
        catch (ProdutoNaoEncontradoException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (EstoqueInsuficienteException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void GerarRelatorios()
    {
        Console.WriteLine("=== Relatórios ===");
        Console.Write("1. Lista de produtos com quantidade em estoque abaixo de: ");
        int limiteQuantidade = int.Parse(Console.ReadLine());

        var produtosAbaixoLimite = estoque.Where(p => p.QuantidadeEmEstoque < limiteQuantidade);
        ImprimirRelatorio("Produtos abaixo do limite de estoque", produtosAbaixoLimite);

        Console.Write("2. Lista de produtos com valor entre mínimo: ");
        double minimo = double.Parse(Console.ReadLine());
        Console.Write("e máximo: ");
        double maximo = double.Parse(Console.ReadLine());

        var produtosEntreValores = estoque.Where(p => p.PrecoUnitario >= minimo && p.PrecoUnitario <= maximo);
        ImprimirRelatorio("Produtos com valor entre mínimo e máximo", produtosEntreValores);

        double valorTotalEstoque = estoque.Sum(p => p.QuantidadeEmEstoque * p.PrecoUnitario);
        Console.WriteLine($"3. Valor total do estoque: {valorTotalEstoque:C}");

        foreach (var produto in estoque)
        {
            double valorTotalProduto = produto.QuantidadeEmEstoque * produto.PrecoUnitario;
            Console.WriteLine($"{produto.Nome}: {valorTotalProduto:C}");
        }
    }

    static void ImprimirRelatorio(string titulo, IEnumerable<(int Codigo, string Nome, int QuantidadeEmEstoque, double PrecoUnitario)> produtos)
    {
        Console.WriteLine($"=== {titulo} ===");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"{produto.Nome} - Quantidade: {produto.QuantidadeEmEstoque}");
        }
    }
}

// Exceção personalizada para produto não encontrado
class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string message) : base(message)
    {
    }
}

// Exceção personalizada para estoque insuficiente
class EstoqueInsuficienteException : Exception
{
    public EstoqueInsuficienteException(string message) : base(message)
    {
    }
}

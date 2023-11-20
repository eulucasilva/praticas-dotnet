using System;
using System.Collections.Generic;
using System.Linq;

class Program {
    class Tarefa
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime dataVencimento { get; set; }
        public bool concluida { get; set; }
    }

    static List<Tarefa> listaDeTarefas = new List<Tarefa>();

    static void Main()
    {
        while (true)
        {
            MostrarMenu();

            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    CriarTarefa();
                    break;
                case "2":
                    ListarTarefas();
                    break;
                case "3":
                    MarcarComoConcluida();
                    break;
                case "4":
                    MostrarTarefasPendentes();
                    break;
                case "5":
                    MostrarTarefasConcluidas();
                    break;
                case "6":
                    ExcluirTarefa();
                    break;
                case "7":
                    PesquisarTarefas();
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n===== Sistema de Gerenciamento de Tarefas =====");
        Console.WriteLine("1. Criar Tarefa");
        Console.WriteLine("2. Listar Tarefas");
        Console.WriteLine("3. Marcar Tarefa como Concluída");
        Console.WriteLine("4. Mostrar Tarefas Pendentes");
        Console.WriteLine("5. Mostrar Tarefas Concluídas");
        Console.WriteLine("6. Excluir Tarefa");
        Console.WriteLine("7. Pesquisar Tarefas");
        Console.WriteLine("8. Sair");
    }

    static void CriarTarefa()
    {
        Console.WriteLine("\n===== Criar Tarefa =====");
        Console.Write("Título: ");
        string _titulo = Console.ReadLine();

        Console.Write("Descrição: ");
        string _descricao = Console.ReadLine();

        Console.Write("Data de Vencimento (dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime _dataVencimento))
        {
            Tarefa novaTarefa = new Tarefa
            {
                titulo = _titulo,
                descricao = _descricao,
                dataVencimento = _dataVencimento
            };

            listaDeTarefas.Add(novaTarefa);
            Console.WriteLine("Tarefa criada com sucesso!");
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tarefa não criada.");
        }
    }

    static void ListarTarefas()
    {
        Console.WriteLine("\n===== Lista de Tarefas =====");
        if (listaDeTarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa encontrada.");
        }
        else
        {
            foreach (var tarefa in listaDeTarefas)
            {
                Console.WriteLine($"Título: {tarefa.titulo}\nDescrição: {tarefa.descricao}\nData de Vencimento: {tarefa.dataVencimento.ToShortDateString()}\nConcluída: {tarefa.concluida}\n");
            }
        }
    }

    static void MarcarComoConcluida()
    {
        Console.WriteLine("\n===== Marcar Tarefa como Concluída =====");
        Console.Write("Informe o título da tarefa a ser marcada como concluída: ");
        string titulo = Console.ReadLine();

        Tarefa tarefaEncontrada = listaDeTarefas.FirstOrDefault(t => t.titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefaEncontrada != null)
        {
            tarefaEncontrada.concluida = true;
            Console.WriteLine("Tarefa marcada como concluída!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void MostrarTarefasPendentes()
    {
        Console.WriteLine("\n===== Tarefas Pendentes =====");
        var tarefasPendentes = listaDeTarefas.Where(t => !t.concluida);

        if (tarefasPendentes.Count() == 0)
        {
            Console.WriteLine("Nenhuma tarefa pendente encontrada.");
        }
        else
        {
            foreach (var tarefa in tarefasPendentes)
            {
                Console.WriteLine($"Título: {tarefa.titulo}\nDescrição: {tarefa.descricao}\nData de Vencimento: {tarefa.dataVencimento.ToShortDateString()}\n");
            }
        }
    }

    static void MostrarTarefasConcluidas()
    {
        Console.WriteLine("\n===== Tarefas Concluídas =====");
        var tarefasConcluidas = listaDeTarefas.Where(t => t.concluida);

        if (tarefasConcluidas.Count() == 0)
        {
            Console.WriteLine("Nenhuma tarefa concluída encontrada.");
        }
        else
        {
            foreach (var tarefa in tarefasConcluidas)
            {
                Console.WriteLine($"Título: {tarefa.titulo}\nDescrição: {tarefa.descricao}\nData de Vencimento: {tarefa.dataVencimento.ToShortDateString()}\n");
            }
        }
    }

    static void ExcluirTarefa()
    {
        Console.WriteLine("\n===== Excluir Tarefa =====");
        Console.Write("Informe o título da tarefa a ser excluída: ");
        string titulo = Console.ReadLine();

        Tarefa tarefaParaExcluir = listaDeTarefas.FirstOrDefault(t => t.titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefaParaExcluir != null)
        {
            listaDeTarefas.Remove(tarefaParaExcluir);
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void PesquisarTarefas(){
        Console.WriteLine("\n===== Pesquisar Tarefas =====");
        Console.Write("Digite uma palavra-chave para a pesquisa: ");
        string palavraChave = Console.ReadLine();

        var tarefasEncontradas = listaDeTarefas.Where(t => t.titulo.Contains(palavraChave, StringComparison.OrdinalIgnoreCase) || t.descricao.Contains(palavraChave, StringComparison.OrdinalIgnoreCase));

        if (tarefasEncontradas.Count() == 0){
            Console.WriteLine("Nenhuma tarefa encontrada com a palavra-chave informada");
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataAdmissao { get; set; }
    public string Tipo { get; set; } // Ex: "CLT", "PJ", "Estagiário"
}

class Program
{
    static List<Funcionario> funcionarios = new List<Funcionario>();

    static void Main(string[] args)
    {
        // Dados iniciais de exemplo
        funcionarios.Add(new Funcionario { Id = 1, Nome = "Ana", Salario = 2500, DataAdmissao = DateTime.Now, Tipo = "CLT" });
        funcionarios.Add(new Funcionario { Id = 2, Nome = "Bruno", Salario = 3500, DataAdmissao = DateTime.Now.AddDays(-5), Tipo = "PJ" });
        funcionarios.Add(new Funcionario { Id = 3, Nome = "Carlos", Salario = 2000, DataAdmissao = DateTime.Now.AddDays(-10), Tipo = "Estagiário" });
        funcionarios.Add(new Funcionario { Id = 4, Nome = "Daniela", Salario = 5000, DataAdmissao = DateTime.Now, Tipo = "CLT" });
        funcionarios.Add(new Funcionario { Id = 5, Nome = "Eduardo", Salario = 4500, DataAdmissao = DateTime.Now, Tipo = "PJ" });

        // Exemplos de chamadas
        ObterPorNome("Bruno");
        ObterPorNome("Zé");

        ObterFuncionariosRecentes();

        ObterEstatisticas();

        ValidarSalarioAdmissao(new Funcionario { Id = 6, Nome = "Maria", Salario = 0, DataAdmissao = DateTime.Now, Tipo = "CLT" });
        ValidarSalarioAdmissao(new Funcionario { Id = 7, Nome = "Pedro", Salario = 2000, DataAdmissao = DateTime.Now.AddDays(-30), Tipo = "CLT" });

        ValidarNome(new Funcionario { Id = 8, Nome = "A", Salario = 1500, DataAdmissao = DateTime.Now, Tipo = "Estagiário" });

        ObterPorTipo("PJ");
    }

    // a) Buscar funcionário pelo nome
    static void ObterPorNome(string nome)
    {
        var funcionario = funcionarios.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (funcionario != null)
        {
            Console.WriteLine($"Funcionário encontrado: {funcionario.Nome}, Salário: {funcionario.Salario}");
        }
        else
        {
            Console.WriteLine($"Nenhum funcionário encontrado com o nome {nome}");
        }
    }

    // b) Remover Id < 4 e exibir lista ordenada por salário decrescente
    static void ObterFuncionariosRecentes()
    {
        funcionarios.RemoveAll(f => f.Id < 4);
        var ordenados = funcionarios.OrderByDescending(f => f.Salario).ToList();

        Console.WriteLine("\nFuncionários recentes (Id >= 4) ordenados por salário:");
        foreach (var f in ordenados)
        {
            Console.WriteLine($"{f.Nome} - Salário: {f.Salario}");
        }
    }

    // c) Estatísticas
    static void ObterEstatisticas()
    {
        int quantidade = funcionarios.Count;
        decimal somaSalarios = funcionarios.Sum(f => f.Salario);

        Console.WriteLine($"\nQuantidade de funcionários: {quantidade}");
        Console.WriteLine($"Soma dos salários: R$ {somaSalarios:F2}");
    }

    // d) Validação de salário e admissão
    static void ValidarSalarioAdmissao(Funcionario funcionario)
    {
        if (funcionario.Salario <= 0)
        {
            Console.WriteLine($"Erro: Salário inválido para {funcionario.Nome}.");
            return;
        }

        if (funcionario.DataAdmissao < DateTime.Now.Date)
        {
            Console.WriteLine($"Erro: Data de admissão inválida para {funcionario.Nome}.");
            return;
        }

        funcionarios.Add(funcionario);
        Console.WriteLine($"Funcionário {funcionario.Nome} adicionado com sucesso!");
    }

    // e) Validação de nome
    static void ValidarNome(Funcionario funcionario)
    {
        if (string.IsNullOrWhiteSpace(funcionario.Nome) || funcionario.Nome.Length < 2)
        {
            Console.WriteLine("Erro: O nome deve ter pelo menos 2 caracteres.");
        }
        else
        {
            funcionarios.Add(funcionario);
            Console.WriteLine($"Funcionário {funcionario.Nome} adicionado com sucesso!");
        }
    }

    // f) Selecionar funcionários por tipo
    static void ObterPorTipo(string tipo)
    {
        var lista = funcionarios.Where(f => f.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase)).ToList();

        Console.WriteLine($"\nFuncionários do tipo {tipo}:");
        if (lista.Any())
        {
            foreach (var f in lista)
            {
                Console.WriteLine($"{f.Nome} - Salário: {f.Salario}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum funcionário encontrado desse tipo.");
        }
    }
}

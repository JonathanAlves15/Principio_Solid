using System;
using System.Collections.Generic;

// Abstração para envio de e-mails
public interface IEmailService
{
    void Enviar(string destinatario, string assunto, string corpo);
}

// Implementação concreta da interface de envio de e-mails
public class EmailService : IEmailService
{
    public void Enviar(string destinatario, string assunto, string corpo)
    {
        // Simulação de envio (não usa SmtpClient diretamente)
        Console.WriteLine($"[EMAIL ENVIADO]\nPara: {destinatario}\nAssunto: {assunto}\nCorpo:\n{corpo}\n");
    }
}

// Interface para cálculo de salário anual
public interface ISalarioCalculator
{
    double CalcularSalarioAnual();
}

// Classe base de funcionário
public abstract class Funcionario : ISalarioCalculator
{
    public string Nome { get; }
    public double Salario { get; }
    public string Email { get; }

    protected Funcionario(string nome, double salario, string email)
    {
        Nome = nome;
        Salario = salario;
        Email = email;
    }

    public abstract double CalcularSalarioAnual();
    public abstract string Cargo { get; }
}

// Subclasses específicas para cada tipo de funcionário (LSP e OCP)
public class Gerente : Funcionario
{
    public Gerente(string nome, double salario, string email) : base(nome, salario, email) { }
    public override double CalcularSalarioAnual() => Salario * 14;
    public override string Cargo => "Gerente";
}

public class Desenvolvedor : Funcionario
{
    public Desenvolvedor(string nome, double salario, string email) : base(nome, salario, email) { }
    public override double CalcularSalarioAnual() => Salario * 12;
    public override string Cargo => "Desenvolvedor";
}

public class Estagiario : Funcionario
{
    public Estagiario(string nome, double salario, string email) : base(nome, salario, email) { }
    public override double CalcularSalarioAnual() => Salario * 10;
    public override string Cargo => "Estagiário";
}

// Responsável apenas por gerar relatórios (SRP)
public class GeradorRelatorio
{
    public string Gerar(Funcionario funcionario)
    {
        return $"[Relatório de Funcionário]\n" +
               $"Nome: {funcionario.Nome}\n" +
               $"Cargo: {funcionario.Cargo}\n" +
               $"Salário Mensal: {funcionario.Salario}\n" +
               $"Salário Anual: {funcionario.CalcularSalarioAnual()}";
    }
}

// Responsável por registrar funcionários (SRP)
public class RegistroDeFuncionarios
{
    private List<Funcionario> funcionarios = new List<Funcionario>();

    public void Adicionar(Funcionario funcionario)
    {
        funcionarios.Add(funcionario);
    }

    public IEnumerable<Funcionario> ObterTodos() => funcionarios;
}

// Programa principal (com injeção de dependência - DIP)
class Program
{
    static void Main(string[] args)
    {
        var registro = new RegistroDeFuncionarios();
        registro.Adicionar(new Gerente("Carlos", 9000, "carlos@empresa.com"));
        registro.Adicionar(new Desenvolvedor("Ana", 5000, "ana@empresa.com"));
        registro.Adicionar(new Estagiario("Lucas", 1200, "lucas@empresa.com"));

        IEmailService emailService = new EmailService();
        var geradorRelatorio = new GeradorRelatorio();

        foreach (var funcionario in registro.ObterTodos())
        {
            string relatorio = geradorRelatorio.Gerar(funcionario);
            Console.WriteLine(relatorio);
            emailService.Enviar(funcionario.Email, "Relatório de Funcionário", relatorio);
        }
    }
}

using System;
using System.Collections.Generic;

namespace RHCaotico
{
    // Abstração para envio de e-mails
    public interface IEmailService
    {
        string Enviar(string destinatario, string assunto, string corpo);
    }

    // Implementação concreta da interface de envio de e-mails
    public class EmailService : IEmailService
    {
        public string Enviar(string destinatario, string assunto, string corpo)
        {
            // Simulação de envio (não usa SmtpClient diretamente)
            return $"[Email Enviado]\n\nPara: {destinatario}\nAssunto: {assunto}\nCorpo:\n{corpo}\n";
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

    // Subclasses específicas para cada tipo de funcionário
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

    // Responsável apenas por gerar relatórios
    public class GeradorRelatorio
    {
        public string Gerar(Funcionario funcionario)
        {
            return $"Nome: {funcionario.Nome}\n" +
                   $"Cargo: {funcionario.Cargo}\n" +
                   $"Salário Mensal: {funcionario.Salario}\n" +
                   $"Salário Anual: {funcionario.CalcularSalarioAnual()}";
        }
    }

    // Responsável por registrar funcionários
    public class RegistroDeFuncionarios
    {
        private List<Funcionario> funcionarios = new List<Funcionario>();

        public void Adicionar(Funcionario funcionario)
        {
            funcionarios.Add(funcionario);
        }

        public IEnumerable<Funcionario> ObterTodos() => funcionarios;
    }
}

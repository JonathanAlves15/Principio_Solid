using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace RHCaotico
{
    // Classe modelo que faz tudo
    public class Funcionario
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string Email { get; set; }

        public Funcionario(string nome, string cargo, double salario, string email)
        {
            Nome = nome;
            Cargo = cargo;
            Salario = salario;
            Email = email;
        }

        public double CalcularSalarioAnual()
        {
            if (Cargo == "Gerente")
                return Salario * 14;
            else if (Cargo == "Desenvolvedor")
                return Salario * 12;
            else if (Cargo == "Estagiario")
                return Salario * 10;
            else
                return Salario * 11;
        }

        public void GerarRelatorioEEnviarPorEmail()
        {
            string relatorio = $"[Relatório]\nNome: {Nome}\nCargo: {Cargo}\nSalário Anual: {CalcularSalarioAnual()}";

            SmtpClient client = new SmtpClient("smtp.empresa.com");
            MailMessage mail = new MailMessage("rh@empresa.com", Email, "Relatório de Funcionário", relatorio);
            client.Send(mail);
        }
    }

    // Classe de registro que também gera relatórios e manipula regras de negócio
    public class RegistroDeFuncionarios
    {
        private List<Funcionario> funcionarios = new List<Funcionario>();

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            funcionarios.Add(funcionario);
        }

        public void MostrarFuncionarios()
        {
            foreach (var f in funcionarios)
            {
                Console.WriteLine($"Nome: {f.Nome}, Cargo: {f.Cargo}, Salário: {f.Salario}");
                if (f.Salario < 2000)
                {
                    Console.WriteLine("Este funcionário está abaixo do piso salarial!");
                }
                f.GerarRelatorioEEnviarPorEmail(); // Viola LSP e acoplamento
            }
        }
    }

    // Programa principal (com lógica de negócio acoplada)
    class Program
    {
        static void Main(string[] args)
        {
            var f1 = new Funcionario("Carlos", "Gerente", 9000, "carlos@empresa.com");
            var f2 = new Funcionario("Ana", "Desenvolvedor", 5000, "ana@empresa.com");
            var f3 = new Funcionario("Lucas", "Estagiario", 1200, "lucas@empresa.com");

            var registro = new RegistroDeFuncionarios();
            registro.AdicionarFuncionario(f1);
            registro.AdicionarFuncionario(f2);
            registro.AdicionarFuncionario(f3);

            registro.MostrarFuncionarios();
        }
    }
}

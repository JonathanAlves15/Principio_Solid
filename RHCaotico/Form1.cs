using System;
using System.Text;
using System.Windows.Forms;

namespace RH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistroDeFuncionarios registro = new RegistroDeFuncionarios();
            registro.Adicionar(new Gerente("Carlos", 9000, "carlos@empresa.com"));
            registro.Adicionar(new Desenvolvedor("Ana", 5000, "ana@empresa.com"));
            registro.Adicionar(new Estagiario("Lucas", 1200, "lucas@empresa.com"));

            IEmailService emailService = new EmailService();
            var geradorRelatorio = new GeradorRelatorio();

            StringBuilder relatorio = new StringBuilder();

            relatorio.AppendLine("\n[Relatório de Funcionários]\n");
            foreach (var funcionario in registro.ObterTodos())
            {
                string relatorioString = geradorRelatorio.Gerar(funcionario);
                relatorio.AppendLine(relatorioString);
                relatorio.AppendLine("--------------------------------");
            }

            string email = emailService.Enviar("Carlos", "Relatório de Funcionários", relatorio.ToString());

            MessageBox.Show(email); // Exibe o resultado em uma MessageBox
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EmailService servicoEmail = new EmailService();
            MessageBox.Show(servicoEmail.Enviar("Patrão", "Aumento salarial", "Acabei de virar pai de 9 filhos, preciso de um aumento."));
        }
    }
}

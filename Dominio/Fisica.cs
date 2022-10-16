namespace Trabalho.Dominio
{
    public class Fisica : BasePessoa
    {
        private string cpf;

        public string Cpf
        {
            get => cpf;
            set => cpf = value;
        }

        public Fisica(string nome, string email, DateOnly dataNascimento, string telefone, string cidade, string senha, string cpf)
            : base(nome, email, dataNascimento, telefone, cidade, senha)
        {
            Cpf = cpf;
        }
    }
}

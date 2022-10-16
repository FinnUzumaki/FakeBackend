namespace Trabalho.Dominio
{
    public abstract class BasePessoa
    {
        private long id;
        private string nome;
        private string email;
        private DateOnly dataNascimento;
        private string telefone;
        private string cidade;
        private string senha;

        public long Id
        {
            get => id;
            set => id = value;
        }
        public string Nome
        {
            get => nome;
            set => nome = value;
        }
        public string Email
        {
            get => email;
            set => email = value;
        }
        public DateOnly DataNascimento
        {
            get => dataNascimento;
            set => dataNascimento = value;
        }
        public string Telefone
        {
            get => telefone;
            set => telefone = value;
        }
        public string Cidade
        {
            get => cidade;
            set => cidade = value;
        }
        public string Senha
        {
            get => senha;
            set => senha = value;
        }

        protected BasePessoa(string nome, string email, DateOnly dataNascimento, string telefone, string cidade, string senha)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Cidade = cidade;
            Senha = senha;
        }
    }
}

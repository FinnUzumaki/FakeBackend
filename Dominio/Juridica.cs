namespace Trabalho.Dominio
{
    public class Juridica : BasePessoa
    {
        private string cnpj;
        private int restaurantes;

        public int Restaurantes
        {
            get => restaurantes;
            set => restaurantes = value;
        }

        public string Cnpj
        {
            get => cnpj;
            set => cnpj = value;
        }

        public Juridica(string nome, string email, DateOnly dataNascimento, string telefone, string cidade, string senha, string cnpj)
            : base(nome, email, dataNascimento, telefone, cidade, senha)
        {
            Cnpj = cnpj;
        }
    }
}

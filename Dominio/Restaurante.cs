namespace Trabalho.Dominio
{
    public class Restaurante
    {
        private long id;
        private long idPessoa;
        private string endereco;
        private string nome;
        private string descricao;

        public long Id
        {
            get => id;
            set => id = value;
        }
        public long IdPessoa
        {
            get => idPessoa;
            set => idPessoa = value;
        }
        public string Endereco
        {
            get => endereco;
            set => endereco = value;
        }
        public string Nome
        {
            get => nome;
            set => nome = value;
        }
        public string Descricao
        {
            get => descricao;
            set => descricao = value;
        }
        public Restaurante(string endereco, string nome, string descricao)
        {
            Endereco = endereco;
            Nome = nome;
            Descricao = descricao;
        }
    }
}

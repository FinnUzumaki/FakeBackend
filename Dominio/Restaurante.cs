namespace Trabalho.Dominio
{
    public class Restaurante
    {
        private ulong id;
        private ulong idPessoa;
        private string endereco;
        private string nome;
        private string descricao;

        public ulong Id
        {
            get => id;
            set => id = value;
        }
        public ulong IdPessoa
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
        public Restaurante(Juridica pessoa ,string endereco, string nome, string descricao)
        {
            IdPessoa = pessoa.Id;
            Endereco = endereco;
            Nome = nome;
            Descricao = descricao;
        }
    }
}

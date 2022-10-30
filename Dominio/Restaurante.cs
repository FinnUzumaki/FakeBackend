namespace Trabalho.Dominio
{
    public class Restaurante
    {
        private ulong id;
        private ulong idPessoa;
        private string nome;
        private string endereco;
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
        public string Nome
        {
            get => nome;
            set => nome = value;
        }
        public string Endereco
        {
            get => endereco;
            set => endereco = value;
        }
        public string Descricao
        {
            get => descricao;
            set => descricao = value;
        }
        public Restaurante(Juridica pessoa, string nome, string endereco, string descricao)
        {
            IdPessoa = pessoa.Id;
            pessoa.Restaurantes++;
            Nome = nome;
            Endereco = endereco;
            Descricao = descricao;
        }
    }
}

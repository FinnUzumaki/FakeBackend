namespace Trabalho.Dominio
{
    public class Restaurante
    {
        private ulong id;
        private ulong idPessoa;
        private string nome;
        private string endereco;
        private string descricao;
        private List<ulong> cardapio;

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
        public List<ulong> Cardapio
        {
            get => cardapio;
        }
        public Restaurante(Juridica pessoa, List<Item> itens, string nome, string endereco, string descricao)
        {
            IdPessoa = pessoa.Id;
            pessoa.Restaurantes++;
            Nome = nome;
            Endereco = endereco;
            Descricao = descricao;
            cardapio = new List<ulong>(itens.Count);
            foreach(Item item in itens)
            {
                cardapio.Add(item.Id);
            }
        }
    }
}

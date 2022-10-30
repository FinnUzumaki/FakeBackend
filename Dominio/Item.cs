namespace Trabalho.Dominio
{
    public class Item
    {
        private ulong id;
        private ulong idRestaurante;
        private string nome;
        private string descricao;
        private float valor;
        private string imagem;

        public ulong Id
        {
            get => id;
            set => id = value;
        }
        public ulong IdRestaurante
        {
            get => idRestaurante;
            set => idRestaurante = value;
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
        public float Valor
        {
            get => valor;
            set => valor = value;
        }
        public string Imagem
        {
            get => imagem;
            set => imagem = value;
        }

        public Item(Restaurante restaurante, string nome, string descricao, float valor, string imagem)
        {
            IdRestaurante = restaurante.Id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
        }
    }
}

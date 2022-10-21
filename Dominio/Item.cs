namespace Trabalho.Dominio
{
    public class Item
    {
        private long id;
        private long idRestaurante;
        private string nome;
        private string descricao;
        private int valor;
        private string imagem;

        public long Id
        {
            get => id;
            set => id = value;
        }
        public long IdRestaurante
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
        public int Valor
        {
            get => valor;
            set => valor = value;
        }
        public string Imagem
        {
            get => imagem;
            set => imagem = value;
        }

        public Item(long idRestaurante, string nome, string descricao, int valor, string imagem)
        {
            IdRestaurante = idRestaurante;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
        }
    }
}

namespace Trabalho.Dominio
{
    public class Item
    {
        private ulong id;
        private string nome;
        private string descricao;
        private float valor;
        private string imagem;

        public ulong Id
        {
            get => id;
            set => id = value;
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

        public Item(string nome, string descricao, float valor, string imagem)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
        }
    }
}

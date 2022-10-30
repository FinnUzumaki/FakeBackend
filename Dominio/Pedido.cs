namespace Trabalho.Dominio
{
    public class Pedido
    {
        private ulong id;
        private ulong idPessoa;
        private List<ulong> itens;
        private float total;

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
        public List<ulong> Itens
        { 
            get => itens;
        }
        public float Total
        {
            get => total;
        }

        public Pedido(Fisica pessoa, List<Item> itens)
        {
            IdPessoa = pessoa.Id;
            pessoa.Pedidos++;
            this.itens = new List<ulong>(itens.Count());
            total = 0;
            foreach(Item item in itens)
            {
                this.itens.Add(item.Id);
                total += item.Valor;
            }
        }

        public void MudarItens(List<Item> itens)
        {
            this.itens = new List<ulong>(itens.Count());
            total = 0;
            foreach (Item item in itens)
            {
                this.itens.Add(item.Id);
                total += item.Valor;
            }
        }
    }
}

namespace Trabalho.Dominio
{
    public class Pedido
    {
        private ulong id;
        private ulong idPessoa;
        private List<ulong> itens;

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

        public Pedido(Fisica pessoa, List<Item> itens)
        {
            IdPessoa = pessoa.Id;
            pessoa.Pedidos++;
            this.itens = new List<ulong>(itens.Count());
            foreach(Item item in itens)
            {
                this.itens.Add(item.Id);
            }
        }
    }
}

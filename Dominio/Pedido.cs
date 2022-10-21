namespace Trabalho.Dominio
{
    public class Pedido
    {
        private long id;
        private long idPessoa;

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

        public Pedido(long idPessoa)
        {
            IdPessoa = idPessoa;
        }
    }
}

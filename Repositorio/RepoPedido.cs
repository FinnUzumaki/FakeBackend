using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoPedido
    {
        public static Pedido Create(Pedido instancia)
        {
            instancia.Id = FakeDB<Pedido>.Lista.Count == 0 ? 0 : FakeDB<Pedido>.Lista.Last().Id + 1;
            FakeDB<Pedido>.Lista.Add(instancia);
            return FakeDB<Pedido>.Lista.Last();
        }

        public static Pedido? Read(ulong id)
        {
            return FakeDB<Pedido>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<Pedido> ReadAll(ulong? idPessoa)
        {
            if (idPessoa != null) return FakeDB<Pedido>.Lista.FindAll(i => i.IdPessoa == idPessoa);
            return FakeDB<Pedido>.Lista;
        }


        //não utilizado
        public static Pedido? Update(ulong id, Pedido instancia)
        {
            Pedido? original = Read(id);
            if (original != null)
            {
                int index = FakeDB<Pedido>.Lista.IndexOf(original);
                instancia.Id = original.Id;
                FakeDB<Pedido>.Lista[index] = instancia;
            }
            return Read(id);
        }
        public static Pedido? Delete(ulong id)
        {
            Pedido? original = Read(id);
            if (original != null) FakeDB<Pedido>.Lista.Remove(original);
            return original;
        }
    }
}

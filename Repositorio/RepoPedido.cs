using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoPedido
    {
        public static Pedido Create(Fisica pessoa, Pedido instancia)
        {
            instancia.IdPessoa = pessoa.Id;
            instancia.Id = FakeDB<Pedido>.Lista.Count == 0 ? 0 : FakeDB<Pedido>.Lista.Last().Id + 1;
            FakeDB<Pedido>.Lista.Add(instancia);
            return FakeDB<Pedido>.Lista.Last();
        }

        public static Pedido? Read(long id)
        {
            return FakeDB<Pedido>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<Pedido> ReadAll(Fisica? pessoa)
        {
            if (pessoa != null) return FakeDB<Pedido>.Lista.FindAll(i => i.IdPessoa == pessoa.Id);
            return FakeDB<Pedido>.Lista;
        }

        public static Pedido? Update(long id, Pedido instancia)
        {
            Pedido? original = Read(id);
            if (original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
            return original;
        }
        public static Pedido? Delete(long id)
        {
            Pedido? original = Read(id);
            if (original != null) FakeDB<Pedido>.Lista.Remove(original);
            return original;
        }
    }
}

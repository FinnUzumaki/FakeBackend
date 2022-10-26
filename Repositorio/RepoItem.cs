using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoItem
    {
        public static Item Create(Item instancia)
        {
            instancia.Id = FakeDB<Item>.Lista.Count == 0 ? 0 : FakeDB<Item>.Lista.Last().Id + 1;
            FakeDB<Item>.Lista.Add(instancia);
            return FakeDB<Item>.Lista.Last();
        }

        public static Item? Read(ulong id)
        {
            return FakeDB<Item>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<Item> ReadAll(ulong? idRestaurante)
        {
            if (idRestaurante != null) return FakeDB<Item>.Lista.FindAll(i => i.IdRestaurante == idRestaurante);
            return FakeDB<Item>.Lista;
        }

        public static Item? Update(ulong id, Item instancia)
        {
            Item? original = Read(id);
            if (original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
            return original;
        }
        public static Item? Delete(ulong id)
        {
            Item? original = Read(id);
            if (original != null) FakeDB<Item>.Lista.Remove(original);
            return original;
        }
    }
}

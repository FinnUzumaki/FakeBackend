using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoItem
    {
        public static Item Create(Restaurante restaurante, Item instancia)
        {
            instancia.IdRestaurante = restaurante.Id;
            instancia.Id = FakeDB<Item>.Lista.Count == 0 ? 0 : FakeDB<Item>.Lista.Last().Id + 1;
            FakeDB<Item>.Lista.Add(instancia);
            return FakeDB<Item>.Lista.Last();
        }

        public static Item? Read(long id)
        {
            return FakeDB<Item>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<Item> ReadAll(Restaurante? restaurante = null)
        {
            if (restaurante != null) return FakeDB<Item>.Lista.FindAll(i => i.IdRestaurante == restaurante.Id);
            return FakeDB<Item>.Lista;
        }

        public static Item? Update(long id, Item instancia)
        {
            Item? original = Read(id);
            if (original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
            return original;
        }
        public static Item? Delete(long id)
        {
            Item? original = Read(id);
            if (original != null) FakeDB<Item>.Lista.Remove(original);
            return original;
        }
    }
}

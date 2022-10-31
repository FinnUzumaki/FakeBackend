using System.ComponentModel;
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

        public static Item? Read(string nome)
        {
            return FakeDB<Item>.Lista.Find(instancia => instancia.Nome == nome);
        }

        public static List<Item> ReadAll(ulong id)
        {
            return FakeDB<Item>.Lista.FindAll(instancia => instancia.IdRestaurante == id);
        }

        public static List<Item> ReadAll(List<ulong>? itens = null)
        {
            if(itens == null) return FakeDB<Item>.Lista;
            List<Item> lista = new List<Item>(itens.Count);
            foreach (ulong id in itens) lista.Add(FakeDB<Item>.Lista.Find(instancia => instancia.Id == id));
            return lista;
        }

        public static Item? Update(ulong id, Item instancia)
        {
            Item? original = Read(id);
            if (original != null)
            {
                int index = FakeDB<Item>.Lista.IndexOf(original);
                instancia.Id = original.Id;
                FakeDB<Item>.Lista[index] = instancia;
            }
            return Read(id);
        }
        public static Item? Delete(ulong id)
        {
            Item? original = Read(id);
            if (original != null) FakeDB<Item>.Lista.Remove(original);
            return original;
        }
    }
}

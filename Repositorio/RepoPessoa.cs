using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoPessoa<TPessoa> where TPessoa : BasePessoa
    {
        public static TPessoa Create(TPessoa instancia)
        {
            instancia.Id = FakeDB<TPessoa>.Lista.Count == 0 ? 0 : FakeDB<TPessoa>.Lista.Last().Id + 1;
            FakeDB<TPessoa>.Lista.Add(instancia);
            return FakeDB<TPessoa>.Lista.Last();
        }

        public static TPessoa? Read(ulong id)
        {
            return FakeDB<TPessoa>.Lista.Find(instancia => instancia.Id == id);
        }
        public static TPessoa? Read(string nome)
        {
            return FakeDB<TPessoa>.Lista.Find(instancia => instancia.Nome == nome);
        }

        public static List<TPessoa> ReadAll()
        {
            return FakeDB<TPessoa>.Lista;
        }

        public static TPessoa? Update(ulong id, TPessoa instancia)
        {
            TPessoa? original = Read(id);
            int index = FakeDB<TPessoa>.Lista.IndexOf(original);
            if (original != null)
            {
                instancia.Id = original.Id;
                FakeDB<TPessoa>.Lista[index] = instancia;
            }
            return original;
        }
        public static TPessoa? Delete(ulong id)
        {
            TPessoa? original = Read(id);
            if (original != null) FakeDB<TPessoa>.Lista.Remove(original);
            return original;
        }
    }
}
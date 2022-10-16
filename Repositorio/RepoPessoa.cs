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

        public static TPessoa? Read(long id)
        {
            return FakeDB<TPessoa>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<TPessoa> ReadAll()
        {
            return FakeDB<TPessoa>.Lista;
        }

        public static TPessoa? Update(long id, TPessoa instancia)
        {
            TPessoa? original = Read(id);
            if(original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
                return original;
        }
        public static TPessoa? Delete(long id)
        {
            TPessoa? original = Read(id);
            if (original != null) FakeDB<TPessoa>.Lista.Remove(original);
            return original;
        }
    }
}
using Trabalho.Dominio;
using Trabalho.FakeDB;

namespace Trabalho.Repositorio
{
    public static class RepoRestaurante
    {
        public static Restaurante Create(Restaurante instancia)
        {
            instancia.Id = FakeDB<Restaurante>.Lista.Count == 0 ? 0 : FakeDB<Restaurante>.Lista.Last().Id + 1;
            FakeDB<Restaurante>.Lista.Add(instancia);
            return FakeDB<Restaurante>.Lista.Last();
        }

        public static Restaurante? Read(long id)
        {
            return FakeDB<Restaurante>.Lista.Find(instancia => instancia.Id == id);
        }

        public static List<Restaurante> ReadAll(long? idPessoa)
        {
            if (idPessoa != null) return FakeDB<Restaurante>.Lista.FindAll(i => i.IdPessoa == idPessoa);
            return FakeDB<Restaurante>.Lista;
        }

        public static Restaurante? Update(long id, Restaurante instancia)
        {
            Restaurante? original = Read(id);
            if (original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
            return original;
        }
        public static Restaurante? Delete(long id)
        {
            Restaurante? original = Read(id);
            if (original != null) FakeDB<Restaurante>.Lista.Remove(original);
            return original;
        }
    }
}

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

        public static Restaurante? Read(ulong id)
        {
            return FakeDB<Restaurante>.Lista.Find(instancia => instancia.Id == id);
        }
        public static Restaurante? Read(string nome)
        {
            return FakeDB<Restaurante>.Lista.Find(instancia => instancia.Nome == nome);
        }

        public static List<Restaurante> ReadAll(ulong? idPessoa)
        {
            if (idPessoa != null) return FakeDB<Restaurante>.Lista.FindAll(i => i.IdPessoa == idPessoa);
            return FakeDB<Restaurante>.Lista;
        }

        public static Restaurante? Update(ulong id, Restaurante instancia)
        {
            Restaurante? original = Read(id);
            if (original != null)
            {
                instancia.Id = original.Id;
                original = instancia;
            }
            return original;
        }
        public static Restaurante? Delete(ulong id)
        {
            Restaurante? original = Read(id);
            if (original != null) FakeDB<Restaurante>.Lista.Remove(original);
            return original;
        }
    }
}

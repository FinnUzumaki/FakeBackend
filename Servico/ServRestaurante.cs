using Trabalho.Dominio;
using Trabalho.Repositorio;

namespace Trabalho.Servico
{
    public static class ServRestaurante
    {
        public static List<Restaurante> Browse(Juridica? pessoa = null)
        {
            return RepoRestaurante.ReadAll(pessoa?.Id);
        }
        public static Restaurante? Read(long id)
        {
            return RepoRestaurante.Read(id);
        }
        public static Restaurante? Edit(long id, Restaurante instancia)
        {
            return RepoRestaurante.Update(id, instancia);
        }
        public static Restaurante Add(Restaurante instancia)
        {
            return RepoRestaurante.Create(instancia);
        }
        public static Restaurante? Delete(long id)
        {
            return RepoRestaurante.Delete(id);
        }
    }
}

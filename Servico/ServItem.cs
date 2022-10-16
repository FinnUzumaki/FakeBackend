using Trabalho.Dominio;
using Trabalho.Repositorio;

namespace Trabalho.Servico
{
    public static class ServItem
    {
        public static List<Item> Browse(Restaurante? restaurante = null)
        {
            return RepoItem.ReadAll(restaurante);
        }
        public static Item? Read(long id)
        {
            return RepoItem.Read(id);
        }
        public static Item? Edit(long id, Item instancia)
        {
            return RepoItem.Update(id, instancia);
        }
        public static Item Add(Restaurante restaurante, Item instancia)
        {
            return RepoItem.Create(restaurante, instancia);
        }
        public static Item? Delete(long id)
        {
            return RepoItem.Delete(id);
        }
    }
}

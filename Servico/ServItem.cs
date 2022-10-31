using Trabalho.Dominio;
using Trabalho.Repositorio;

namespace Trabalho.Servico
{
    public static class ServItem
    {
        public static List<Item> Browse()
        {
            return RepoItem.ReadAll();
        }
        public static List<Item> Browse(Restaurante restaurante)
        {
            return RepoItem.ReadAll(restaurante.Id);
        }
        public static List<Item> Browse(Pedido pedido)
        {
            return RepoItem.ReadAll(pedido.Itens);
        }
        public static Item? Read(ulong id)
        {
            return RepoItem.Read(id);
        }
        public static Item? Read(string nome)
        {
            return RepoItem.Read(nome);
        }
        public static Item? Edit(ulong id, Item instancia)
        {
            return RepoItem.Update(id, instancia);
        }
        public static Item Add(Item instancia)
        {
            return RepoItem.Create(instancia);
        }
        public static Item? Delete(ulong id)
        {
            return RepoItem.Delete(id);
        }
    }
}

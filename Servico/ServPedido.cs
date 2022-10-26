using Trabalho.Dominio;
using Trabalho.Repositorio;

namespace Trabalho.Servico
{
    public static class ServPedido
    {
        public static List<Pedido> Browse(Fisica? pessoa = null)
        {
            return RepoPedido.ReadAll(pessoa?.Id);
        }
        public static Pedido? Read(ulong id)
        {
            return RepoPedido.Read(id);
        }
        public static Pedido? Edit(ulong id, Pedido instancia)
        {
            return RepoPedido.Update(id, instancia);
        }
        public static Pedido Add(Pedido instancia)
        {
            return RepoPedido.Create(instancia);
        }
        public static Pedido? Delete(ulong id)
        {
            return RepoPedido.Delete(id);
        }
    }
}

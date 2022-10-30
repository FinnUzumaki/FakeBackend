using System.Runtime.CompilerServices;
using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmPedido
    {
        public static void Listar()
        {
            List<string> pedidos = new List<string>(ServPedido.Browse().Count);
            foreach (Pedido pedido in ServPedido.Browse())
                pedidos.Add(
                    $"Id:{pedido.Id}\t" +
                    $"IdPessoa:{pedido.IdPessoa}\t" +
                    $"Itens:{pedido.Itens.Count}\t" +
                    $"Total:{pedido.Total}");
            int index = Program.Menu("Lista de todas as pessoas, selecione uma para mais informações.", pedidos.ToArray());
            if (index == -1) return;
            Achar(ServPedido.Browse()[index]);
        }

        public static void Achar(Pedido? pedido = null)
        {
            if (pedido == null)
            {
                ulong id;
                string line;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Digite o Id do pedido que deseja achar.");
                    line = Console.ReadLine();
                }
                while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                pedido = string.IsNullOrEmpty(line) ? null : ServPedido.Read(id);
            }

            Console.Clear();
            if (pedido == null) { Console.WriteLine("Pedido não encontrado."); Console.ReadKey(true); return; }

            bool onLoop = true;
            bool removido = false;
            Console.WriteLine();
            do
            {
                string line = "";
                line += $"Id:{pedido.Id}\tIdPessoa:{pedido.IdPessoa}\tTotal:{pedido.Total}\n";
                foreach (ulong id in pedido.Itens)
                {
                    Item? item = ServItem.Read(id);
                    if (item != null)
                        line +=
                            $"\tId:{item.Id}\t" +
                            $"Nome:{item.Nome}\t" +
                            $"Descr:{item.Descricao}\t" +
                            $"R${item.Valor}\n";
                }
                switch (Program.Menu("As informações do pedido selecionado são:\n\n" +
                line + '\n' +
                "O que deseja fazer?", new string[] { "Editar", "Remover" }))
                {
                    case 0:
                        pedido = AdmPedido.Editar(pedido);
                        break;
                    case 1:
                        AdmPedido.Remover(pedido, out removido);
                        break;
                    default:
                        onLoop = false;
                        break;
                }
            } while (onLoop && !removido);

        }

        private static Pedido Editar(Pedido pedido)
        {
            List<Item> itens = ServItem.Browse(pedido);
            bool? terminado = false;
            do
            {
                int index = 0;
                float total = 0;
                string line = itens.Count == 0 ? "Nenhum item adicionado.\n" : "";
                foreach (Item item in itens)
                {
                    line += $"{item.Nome}, R${item.Valor:0.##}\n";
                    total += item.Valor;
                }
                switch (Program.Menu(line + "Total = R$" + total.ToString("0.##") + "\n\nSelecione o que deseja fazer.", new string[] { "Adicionar item", "Remover item", "Terminar edição", "Cancelar" }))
                {
                    case 0:
                        List<string> restaurantes = new List<string>(ServRestaurante.Browse().Count);
                        foreach (Restaurante restaurante in ServRestaurante.Browse())
                            restaurantes.Add(restaurante.Nome);

                        bool repeat = false;
                        Restaurante? escolhido = null;
                        do
                        {
                            repeat = false;
                            index = Program.Menu("Escolha o restaurante para mostrar os itens.", restaurantes.ToArray());
                            if (index == -1) break;
                            escolhido = ServRestaurante.Browse()[index];

                            List<string> temp = new List<string>(ServItem.Browse(escolhido).Count);
                            foreach (Item item in ServItem.Browse(escolhido))
                                temp.Add(
                                    $"Nome:{item.Nome}\t" +
                                    $"Descr:{item.Descricao}\t" +
                                    $"R${item.Valor}\t" +
                                    $"Imagem:{item.Imagem}");

                            index = Program.Menu("Selecione o item para adicionar no pedido", temp.ToArray());
                            if (index == -1) repeat = true;
                        } while (repeat);
                        if (index == -1) break;
                        itens.Add(ServItem.Browse(escolhido)[index]);

                        break;
                    case 1:
                        List<string> temp2 = new List<string>(itens.Count);
                        foreach (Item item in itens)
                            temp2.Add($"{item.Nome}, {item.Valor}");
                        index = Program.Menu("Escolha o item a ser removido.", temp2.ToArray());
                        if(index == -1) break;
                        itens.RemoveAt(index);
                        break;
                    case 2:
                        terminado = Program.Menu("Tem certeza de que deseja terminar a edição?", new string[] { "Não", "Sim" }) == 1 ? true : false;
                        break;
                    default:
                        terminado = Program.Menu("Tem certeza de que deseja cancelar a edição?", new string[] { "Não", "Sim" }) == 1 ? null : false;
                        break;
                }
            } while (terminado == false);

            if (terminado == true)
            {
                pedido.MudarItens(itens);
                Console.WriteLine("Edição completada com sucesso.");
            }
            else Console.WriteLine("Edição cancelada com sucesso.");

            Console.ReadKey(true);
            return pedido;
        }

        private static void Remover(Pedido pedido, out bool removido)
        {
            switch (Program.Menu($"Tem certeza que deseja remover o pedido?", new string[] { "Não", "Sim" }))
            {
                case 1:
                    Fisica? p = ServPessoa<Fisica>.Read(pedido.IdPessoa);
                    if (p != null) p.Pedidos--;
                    if (ServPedido.Delete(pedido.Id) == pedido) Console.WriteLine("Pedido removido com sucesso.");
                    else Console.WriteLine("A remoção não pode ser concluida.");
                    Console.ReadKey(true);
                    break;
                default:
                    removido = false;
                    return;
            }
            removido = true;
        }
    }
}

using System.Runtime.CompilerServices;
using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmPedido
    {
        public static void Listar()
        {
            string[] pedidos = new string[ServPedido.Browse().Count + 2];
            pedidos[0] = "Voltar";
            for (int i = 0; i < ServPedido.Browse().Count; i++)
                pedidos[i+1] =
                    $"Id:{ServPedido.Browse()[i].Id}\t" +
                    $"IdPessoa:{ServPedido.Browse()[i].IdPessoa}\t" +
                    $"Itens:{ServPedido.Browse()[i].Itens.Count}\t" +
                    $"Total:{ServPedido.Browse()[i].Total}";
            pedidos[ServPessoa<Fisica>.Browse().Count + 1] = "Voltar";
            int index = Program.Menu("Lista de todos os pedidos, selecione um para mais informações.", pedidos);
            if (index == ServPessoa<Fisica>.Browse().Count + 1 || index == 0) return;

            Achar(ServPedido.Browse()[index - 1]);
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
            if (pedido == null) { Console.WriteLine("Pedido não encontrado."); Console.ReadKey(true); }
            else
            {
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
                    "O que deseja fazer?", new string[] { "Editar", "Remover", "Voltar" }))
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
        }

        private static Pedido Editar(Pedido pedido)
        {
            List<Item> itens = ServItem.Browse(pedido);
            bool? terminado = false;
            do
            {
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

                        Restaurante escolhido = ServRestaurante.Browse()[Program.Menu("Escolha o restaurante para mostrar os itens.", restaurantes.ToArray())];

                        List<string> temp = new List<string>(escolhido.Cardapio.Count);
                        foreach (Item item in ServItem.Browse(escolhido))
                            temp.Add(
                                $"Nome:{item.Nome}\t" +
                                $"Descr:{item.Descricao}\t" +
                                $"R${item.Valor}\t" +
                                $"Imagem:{item.Imagem}");

                        itens.Add(ServItem.Browse(escolhido)[Program.Menu("Selecione o item para adicionar no pedido", temp.ToArray())]);

                        break;
                    case 1:
                        List<string> temp2 = new List<string>(itens.Count);
                        foreach (Item item in itens)
                            temp2.Add($"{item.Nome}, {item.Valor}");
                        itens.RemoveAt(Program.Menu("Escolha o item a ser removido.", temp2.ToArray()));
                        break;
                    case 2:
                        terminado = Program.Menu("Tem certeza de que deseja terminar a edição?", new string[] { "Sim", "Não" }) == 0 ? true : false;
                        break;
                    default:
                        terminado = Program.Menu("Tem certeza de que deseja cancelar a edição?", new string[] { "Sim", "Não" }) == 0 ? null : false;
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
            switch (Program.Menu($"Tem certeza que deseja remover o pedido?", new string[] { "Sim", "Não" }))
            {
                case 0:
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

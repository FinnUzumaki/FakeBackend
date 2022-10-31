using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmItem
    {
        public static void Listar()
        {
            List<string> itens = new List<string>(ServItem.Browse().Count);
            foreach (Item item in ServItem.Browse())
                itens.Add(
                    $"Id:{item.Id}\t" +
                    $"IdRes:{item.IdRestaurante}\t" +
                    $"Nome:{item.Nome}");
            int index = Program.Menu("Lista de todas os itens, selecione uma para mais informações.", itens.ToArray());
            if (index == -1) return;
            Achar(ServItem.Browse()[index]);
        }

        public static void Achar(Item? item = null)
        {
            if (item == null)
            {
                ulong id;
                string? nome, line;
                switch (Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
                {
                    case -1:
                        return;
                    case 0:
                        Console.WriteLine("Digite o nome do item que deseja achar.");
                        nome = Console.ReadLine();
                        item = ServItem.Read(nome);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Digite o Id do item que deseja achar.");
                            line = Console.ReadLine();
                        }
                        while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                        item = string.IsNullOrEmpty(line) ? null : ServItem.Read(id);
                        break;
                }
            }

            Console.Clear();
            if (item == null) { Console.WriteLine("Item não encontrado."); Console.ReadKey(true); return; }

            Console.WriteLine("As informações do item selecionado são:\n\n" +
            $"Id:\t{item.Id}\n" +
            $"IdRestaurante:\t{item.IdRestaurante}\n" +
            $"Nome:\t{item.Nome}\n" +
            $"Descrição:\t{item.Descricao}\n" +
            $"Valor:\t{item.Valor}\n" +
            $"Imagem:\t{item.Imagem}\n");

            Console.ReadKey(true);
        }
    }
}

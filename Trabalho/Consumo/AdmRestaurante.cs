using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmRestaurante
    {
         public static void Listar()
        {
            List<string> restaurantes = new List<string>(ServRestaurante.Browse().Count);
            foreach (Restaurante restaurante in ServRestaurante.Browse())
                restaurantes.Add( 
                    $"Id:{restaurante.Id}\t" +
                    $"IdPessoa:{restaurante.IdPessoa}\t" +
                    $"Nome:{restaurante.Nome}");
            int index = Program.Menu("Lista de todas as pessoas, selecione uma para mais informações.", restaurantes.ToArray());
            if (index == -1) return;
            //Achar(ServRestaurante.Browse()[index]);
        }

        /*public static void Achar(Restaurante? restaurante = null)
        {
            if (restaurante == null)
            {
                ulong id;
                string? nome, line;
                switch (Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
                {
                    case 0:
                        Console.WriteLine("Digite o nome da pessoa que deseja achar.");
                        nome = Console.ReadLine();
                        restaurante = ServRestaurante.Read(nome);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Digite o Id da pessoa que deseja achar.");
                            line = Console.ReadLine();
                        }
                        while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                        restaurante = string.IsNullOrEmpty(line) ? null : ServRestaurante.Read(id);
                        break;
                }
            }

            Console.Clear();
            if (restaurante == null) { Console.WriteLine("Pessoa não encontrada."); Console.ReadKey(true); return; }

            bool onLoop = true;
            bool removido = false;
            Console.WriteLine();
            do
            {
                switch (Program.Menu("As informações da pessoa selecionada são:\n\n" +
                $"Id:\t{restaurante.Id}\n" +
                $"Nome:\t{restaurante.Nome}\n" +
                $"Email:\t{restaurante.Email}\n" +
                $"Nasc.:\t{restaurante.DataNascimento.ToShortDateString()}\n" +
                $"Tel.:\t{restaurante.Telefone}\n" +
                $"Cidade:\t{restaurante.Cidade}\n" +
                $"Senha:\t{restaurante.Senha}\n" +
                $"Cpf:\t{restaurante.Cpf}\n" +
                $"NumPed:\t{restaurante.Pedidos}\n\n" +
                "O que deseja fazer?", new string[] { "Editar", "Remover", "Mostrar Pedidos", "Adicionar Pedido", "Voltar" }))
                {
                    case 0:
                        restaurante = AdmFisica.Editar(restaurante);
                        break;
                    case 1:
                        AdmFisica.Remover(restaurante, out removido);
                        break;
                    case 2:
                        AdmFisica.MostrarPedidos(restaurante);
                        break;
                    case 3:
                        AdmFisica.AdicionarPedido(restaurante);
                        break;
                    default:
                        onLoop = false;
                        break;
                }
            } while (onLoop && !removido);

        }*/

        public static void AdicionarItem(Restaurante restaurante)
        {
            uint n = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Quantos itens deseja adicionar?");
            } while (!uint.TryParse(Console.ReadLine(), out n) || n == 0);
            
            for(int i = 0; i < n; i++)
            {
                bool valido = true;
                string[] temp = new string[] { "Nome: ", "Descrição: ", "Valor: " };
                float valor;
                for (int j = 0; j < temp.Length; j++)
                {
                    Console.Clear();
                    Console.WriteLine("Para adicionar um Item são necessárias algumas informações.");
                    Console.Write(temp[j]);
                    temp[j] = Console.ReadLine();
                    valido &= !string.IsNullOrEmpty(temp[j]);
                }

                valido &= float.TryParse(temp[2], out valor);

                Console.Clear();
                if (valido)
                {
                    Item item = new(restaurante, temp[0], temp[1], valor, "ibagem");
                    if (ServItem.Add(item) == item) Console.WriteLine("Item adicionado com sucesso.");
                    else Console.WriteLine("Houve algum problema ao adicionar o novo restaurante.");
                }
                else Console.WriteLine("As informações dadas não são validas.");
                Console.ReadKey(true);
            }
        }
    }
}

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
            Achar(ServRestaurante.Browse()[index]);
        }

        public static void Achar(Restaurante? restaurante = null)
        {
            if (restaurante == null)
            {
                ulong id;
                string? nome, line;
                switch (Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
                {
                    case -1:
                        return;
                    case 0:
                        Console.WriteLine("Digite o nome do restaurante que deseja achar.");
                        nome = Console.ReadLine();
                        restaurante = ServRestaurante.Read(nome);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Digite o Id do restaurante que deseja achar.");
                            line = Console.ReadLine();
                        }
                        while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                        restaurante = string.IsNullOrEmpty(line) ? null : ServRestaurante.Read(id);
                        break;
                }
            }

            Console.Clear();
            if (restaurante == null) { Console.WriteLine("Restaurante não encontrado."); Console.ReadKey(true); return; }

            bool onLoop = true;
            bool removido = false;

            do
            {
                string cardapio = "";
                foreach (Item item in ServItem.Browse(restaurante))
                {
                    cardapio +=
                        $"\tId:{item.Id}\tNome:{item.Nome}\tDescrição:{item.Descricao}\tValor:{item.Valor}\tImagem:{item.Imagem}\n";
                }

                switch (Program.Menu("As informações do restaurante selecionado são:\n\n" +
                $"Id:\t\t{restaurante.Id}\n" +
                $"IdPessoa.:\t{restaurante.IdPessoa}\n" +
                $"Nome:\t\t{restaurante.Nome}\n" +
                $"Endereço:\t{restaurante.Endereco}\n" +
                $"Descrição:\t{restaurante.Descricao}\n" +
                "Cardápio:\n" + cardapio +
                "O que deseja fazer?", new string[] { "Editar", "Editar cardápio" }))
                {
                    case 0:
                        restaurante = AdmRestaurante.Editar(restaurante);
                        break;
                    case 1:
                        AdmRestaurante.EditarCardapio(restaurante);
                        break;
                    default:
                        onLoop = false;
                        break;
                }
            } while (onLoop && !removido);

        }

        private static Restaurante Editar(Restaurante restaurante)
        {
            Console.WriteLine("Para editar um campo escreva o novo valor, para deixar o antigo apenas deixe em branco.");

            string[] temp = new string[] { "Nome: ", "Endereço: ", "Descrição: "};
            string[] temp2 = new string[] { restaurante.Nome, restaurante.Endereco, restaurante.Descricao};
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + temp2[i]);
                Console.Write("Novo " + temp[i]);
                temp[i] = Console.ReadLine();
            }

            Console.Clear();

            Restaurante editado = new Restaurante(
                ServPessoa<Juridica>.Read(restaurante.IdPessoa),
                string.IsNullOrEmpty(temp[0]) ? restaurante.Nome : temp[0],
                string.IsNullOrEmpty(temp[1]) ? restaurante.Endereco : temp[1],
                string.IsNullOrEmpty(temp[2]) ? restaurante.Descricao : temp[2]);

            restaurante = ServRestaurante.Edit(restaurante.Id, editado);

            if (restaurante == editado) Console.WriteLine("Informações atualizadas com sucesso.");
            else Console.WriteLine("Houve um erro ao atualizar as informações.");

            Console.ReadKey(true);
            return restaurante;
        }

        private static void EditarCardapio(Restaurante restaurante)
        {
            bool repeat = true;
            do
            {
                List<string> cardapio = new List<string>(ServItem.Browse(restaurante).Count);
                foreach (Item item in ServItem.Browse(restaurante))
                {
                    cardapio.Add(
                        $"Id:{item.Id}\tNome:{item.Nome}\tDescrição:{item.Descricao}\tValor:{item.Valor}\tImagem:{item.Imagem}"
                        );
                }
                cardapio.Add("Novo");

                int index = Program.Menu("Selecione o item e o que deseja fazer com ele ou novo para criar um.", cardapio.ToArray());
                if (index == -1) repeat = false;
                else if (index == ServItem.Browse(restaurante).Count) AdicionarItem(restaurante);
                else
                {
                    Item item = ServItem.Browse(restaurante)[index];
                    switch(Program.Menu("Deseja editar ou remover o item?", new string[] { "Editar", "Remover" }))
                    {
                        case 0:
                            AdmRestaurante.EditarItem(item);
                            break;
                        case 1:
                            if (Program.Menu($"Tem certeza que deseja remover {item.Nome}?", new string[] { "Não", "Sim" }) == 1)
                            {
                                ServItem.Delete(item.Id);

                                while (ServItem.Browse(restaurante).Count == 0)
                                {
                                    Console.WriteLine("O cardápio não pode ficar vazio.");
                                    Console.ReadKey(true);
                                    AdmRestaurante.AdicionarItem(restaurante);
                                }
                            }
                            break;
                    }
                }
            } while (repeat);
            
            
        }

        private static void EditarItem(Item item)
        {
            Console.WriteLine("Para editar um campo escreva o novo valor, para deixar o antigo apenas deixe em branco.");

            string line;
            string[] temp = new string[] { "Nome: ", "Descrição: " };
            string[] temp2 = new string[] { item.Nome, item.Descricao};
            float valor;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + temp2[i]);
                Console.Write("Novo " + temp[i]);
                temp[i] = Console.ReadLine();
            }
            do
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + item.Valor.ToString());
                Console.Write("Novo Valor: ");
                line = Console.ReadLine();
            } while (!float.TryParse(line, out valor) && !string.IsNullOrEmpty(line));
            Console.Clear();

            Item editado = new Item(
                ServRestaurante.Read(item.IdRestaurante),
                string.IsNullOrEmpty(temp[0]) ? item.Nome : temp[0],
                string.IsNullOrEmpty(temp[1]) ? item.Descricao : temp[1],
                string.IsNullOrEmpty(line) ? item.Valor : valor,
                item.Imagem);
            ServItem.Edit(item.Id, editado);

            item = ServItem.Read(item.Id);

            if (item == editado) Console.WriteLine("Informações atualizadas com sucesso.");
            else Console.WriteLine("Houve um erro ao atualizar as informações.");

            Console.ReadKey(true);
        }

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
                    else Console.WriteLine("Houve algum problema ao adicionar o novo item.");
                }
                else Console.WriteLine("As informações dadas não são validas.");
                Console.ReadKey(true);
            }
        }
    }
}

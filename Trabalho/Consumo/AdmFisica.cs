using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmFisica
    {
        public static void Popular()
        {
            for (int i = 0; i < 10; i++)
            {

                ServItem.Add(new Item(
                ServRestaurante.Add(new Restaurante(
                    ServPessoa<Juridica>.Add(new Juridica("NomeJ" + i, "EmailJ" + i, new DateOnly(2022, 12, i + 1), "TelefoneJ" + i, "CidadeJ" + i, "SenhaJ" + i, "CnpJ" + i))
                    , "NomeR" + i, "EnderecoR" + i, "Restaurante da pessoa juridica " + i))
                , "NomeI" + i, "Eh um item", 1000.00f, "ibagem"));

                ServPedido.Add(new Pedido(
                    ServPessoa<Fisica>.Add(new Fisica("NomeF" + i, "EmailF" + i, new DateOnly(2000, 1, i + 1), "TelefoneF" + i, "CidadeF" + i, "SenhaF" + i, "CpF" + i))
                    , ServItem.Browse(ServRestaurante.Read((ulong)i))));
            }
        }

        public static void Listar()
        {
            List<string> pessoas = new List<string>(ServPessoa<Fisica>.Browse().Count);
            foreach (Fisica pessoa in ServPessoa<Fisica>.Browse())
                pessoas.Add(
                    $"Id:{pessoa.Id}\t" +
                    $"Nome:{pessoa.Nome}");
            int index = Program.Menu("Lista de todas as pessoas, selecione uma para mais informações.", pessoas.ToArray());
            if (index == -1) return;
            Achar(ServPessoa<Fisica>.Browse()[index]);
        }

        public static void Achar(Fisica? pessoa = null)
        {
            if (pessoa == null)
            {
                ulong id;
                string? nome, line;
                switch (Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
                {
                    case -1:
                        return;
                    case 0:
                        Console.WriteLine("Digite o nome da pessoa que deseja achar.");
                        nome = Console.ReadLine();
                        pessoa = ServPessoa<Fisica>.Read(nome);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Digite o Id da pessoa que deseja achar.");
                            line = Console.ReadLine();
                        }
                        while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                        pessoa = string.IsNullOrEmpty(line) ? null : ServPessoa<Fisica>.Read(id);
                        break;
                }
            }

            Console.Clear();
            if (pessoa == null) { Console.WriteLine("Pessoa não encontrada."); Console.ReadKey(true); return; }

            bool onLoop = true;
            bool removido = false;
            do
            {
                switch (Program.Menu("As informações da pessoa selecionada são:\n\n" +
                $"Id:\t{pessoa.Id}\n" +
                $"Nome:\t{pessoa.Nome}\n" +
                $"Email:\t{pessoa.Email}\n" +
                $"Nasc.:\t{pessoa.DataNascimento.ToShortDateString()}\n" +
                $"Tel.:\t{pessoa.Telefone}\n" +
                $"Cidade:\t{pessoa.Cidade}\n" +
                $"Senha:\t{pessoa.Senha}\n" +
                $"Cpf:\t{pessoa.Cpf}\n" +
                $"NumPed:\t{pessoa.Pedidos}\n\n" +
                "O que deseja fazer?", new string[] { "Editar", "Remover", "Mostrar Pedidos", "Adicionar Pedido" }))
                {
                    case 0:
                        pessoa = AdmFisica.Editar(pessoa);
                        break;
                    case 1:
                        AdmFisica.Remover(pessoa, out removido);
                        break;
                    case 2:
                        AdmFisica.MostrarPedidos(pessoa);
                        break;
                    case 3:
                        AdmFisica.AdicionarPedido(pessoa);
                        break;
                    default:
                        onLoop = false;
                        break;
                }
            } while (onLoop && !removido);

        }

        public static void Adicionar()
        {
            bool valido = true;
            string[] temp = new string[]{ "Nome: ", "Email: ", "Data de Nascimento: ", "Telefone: ", "Cidade: ", "Senha: ", "Cpf: "};
            DateOnly DataNascimento;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Para adicionar um usuário são necessárias algumas informações.");
                Console.Write(temp[i]);
                temp[i] = Console.ReadLine();
                valido &= !string.IsNullOrEmpty(temp[i]);
            }
            valido &= DateOnly.TryParse(temp[2], out DataNascimento);

            Console.Clear();
            if(valido)
            {
                Fisica p = new(temp[0], temp[1], DataNascimento, temp[3], temp[4], temp[5], temp[6]);
                if (ServPessoa<Fisica>.Add(p) == p) Console.WriteLine("Usuário adicionado com sucesso.");
                else Console.WriteLine("Houve algum problema ao adicionar o novo usuário.");
            }
            else Console.WriteLine("As informações dadas não são validas.");
                Console.ReadKey(true);
        }

        private static Fisica Editar(Fisica pessoa)
        {
            Console.WriteLine("Para editar um campo escreva o novo valor, para deixar o antigo apenas deixe em branco.");

            string? line;
            string[] temp = new string[] { "nome: ", "email: ", "telefone: ", "cidade: ", "senha: ", "cpf: " };
            string[] temp2 = new string[] { pessoa.Nome, pessoa.Email, pessoa.Telefone, pessoa.Cidade, pessoa.Senha, pessoa.Cpf };
            DateOnly DataNascimento;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + temp2[i]);
                Console.Write("Novo "+temp[i]);
                temp[i] = Console.ReadLine();

            }
            do
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + pessoa.DataNascimento.ToString());
                Console.Write("Nova data de nascimento: ");
                line = Console.ReadLine();
            } while (!DateOnly.TryParse(line, out DataNascimento) && !string.IsNullOrEmpty(line)) ;
            Console.Clear();

            Fisica editado = new Fisica(
                string.IsNullOrEmpty(temp[0]) ? pessoa.Nome : temp[0],
                string.IsNullOrEmpty(temp[1]) ? pessoa.Email : temp[1],
                string.IsNullOrEmpty(line) ? pessoa.DataNascimento : DataNascimento,
                string.IsNullOrEmpty(temp[2]) ? pessoa.Telefone : temp[2],
                string.IsNullOrEmpty(temp[3]) ? pessoa.Cidade : temp[3],
                string.IsNullOrEmpty(temp[4]) ? pessoa.Senha : temp[4],
                string.IsNullOrEmpty(temp[5]) ? pessoa.Cpf : temp[5]);
            editado.Pedidos = pessoa.Pedidos;
            
            pessoa = ServPessoa<Fisica>.Edit(pessoa.Id, editado);

            if (pessoa == editado) Console.WriteLine("Informações atualizadas com sucesso.");
            else Console.WriteLine("Houve um erro ao atualizar as informações.");
            
            Console.ReadKey(true);
            return pessoa;
        }
    
        private static void Remover(Fisica pessoa, out bool removido)
        {
            switch(Program.Menu($"Tem certeza que deseja remover {pessoa.Nome}?", new string[] {"Não","Sim"}))
            {
                default:
                    removido = false;
                    return;
                case 1:
                    if (ServPessoa<Fisica>.Delete(pessoa.Id) == pessoa) Console.WriteLine("Usuário removido com sucesso.");
                    else Console.WriteLine("A remoção não pode ser concluida.");
                    Console.ReadKey(true);
                    break;
            }
            removido = true;
        }

        private static void MostrarPedidos(Fisica pessoa)
        {
            if (ServPedido.Browse(pessoa).Count == 0) Console.WriteLine("Nenhum pedido.");
            foreach(Pedido pedido in ServPedido.Browse(pessoa))
            {
                Console.WriteLine("Id do pedido: " + pedido.Id);
                foreach(Item item in ServItem.Browse(pedido))
                {
                    if(item != null)
                        Console.WriteLine($"\tId:{item.Id}\tNome:{item.Nome}\tDescr:{item.Descricao}\tR${item.Valor}");
                    else Console.WriteLine("Item não encontrado");
                }
            }
            Console.ReadKey(true);
        }

        private static void AdicionarPedido(Fisica pessoa)
        {
            List<Item> itens = new List<Item>();
            bool? terminado = false;
            do
            {
                int index = 0;
                float total = 0;
                string line = itens.Count == 0 ? "Nenhum item adicionado." : "";
                foreach (Item item in itens)
                {
                    line += $"{item.Nome}, R${item.Valor:0.##}\n";
                    total += item.Valor;
                }
                switch (Program.Menu(line + "\nTotal = R$" + total.ToString("0.##") + "\n\nSelecione o que deseja fazer.", new string[] { "Adicionar item", "Remover item", "Terminar pedido", "Cancelar" }))
                {
                    case 0:
                        List<string> restaurantes = new List<string>(ServRestaurante.Browse().Count);
                        foreach (Restaurante restaurante in ServRestaurante.Browse())
                            restaurantes.Add(restaurante.Nome);

                        bool repeat;
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
                        if (index == -1) break;
                        itens.RemoveAt(index);
                        break;
                    case 2:
                        if(itens.Count == 0) terminado = Program.Menu("A lista está vazia, deseja cancelar o pedido?", new string[] { "Não", "Sim" }) == 1 ? null : false;
                        else terminado = Program.Menu("Tem certeza de que deseja terminar o pedido?", new string[] { "Não", "Sim" }) == 1 ? true : false;
                        break;
                    default:
                        terminado = Program.Menu("Tem certeza de que deseja cancelar o pedido?", new string[] { "Não", "Sim" }) == 1 ? null : false;
                        break;
                }
            } while (terminado == false);

            if (terminado == true)
            {
                Pedido p = new Pedido(pessoa, itens);
                if (ServPedido.Add(p) == p) Console.WriteLine("Pedido criado com sucesso.");
                else Console.WriteLine("Houve um erro ao concluir o pedido.");
            }else Console.WriteLine("Pedido cancelado com sucesso.");
            
            Console.ReadKey(true);
        }
    }
}

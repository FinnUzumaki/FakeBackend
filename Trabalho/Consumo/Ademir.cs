using System.Runtime.CompilerServices;
using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class Ademir
    {
        public static void Popular()
        {
            for (int i = 0; i < 10; i++)
            {
                //pequeno crime
                ServItem.Add(new Item(
                        ServRestaurante.Add(new Restaurante(
                            ServPessoa<Juridica>.Add(new Juridica("NomeJ" + i, "EmailJ" + i, new DateOnly(2022, 12, i + 1), "TelefoneJ" + i, "CidadeJ" + i, "SenhaJ" + i, "CnpJ" + i)), "EnderecoR" + i, "NomeR" + i, "Restaurante da pessoa juridica " + i)), "Item do restaurante " + i, "Eh um item", 1000.00f, "ibagem"));
                ServPedido.Add(new Pedido(
                    ServPessoa<Fisica>.Add(new Fisica("NomeF" + i, "EmailF" + i, new DateOnly(2000, 1, i + 1), "TelefoneF" + i, "CidadeF" + i, "SenhaF" + i, "CpF" + i)), ServItem.Browse(ServRestaurante.Read((ulong)i))));
            }
        }

        public static void ListarFisica()
        {
            List<Fisica> lista = ServPessoa<Fisica>.Browse();
            foreach (Fisica pessoa in lista)
            {
                Console.WriteLine($"{pessoa.Id}\t{pessoa.Nome}\t{pessoa.Email}\t{pessoa.DataNascimento.ToShortDateString()}\t{pessoa.Telefone}\t{pessoa.Cidade}\t{pessoa.Senha}\t{pessoa.Cpf}\t{pessoa.Pedidos}\n");
            }
            Console.ReadKey(true);
        }

        public static void AcharFisica()
        {
            ulong id;
            string? nome;
            Fisica? pessoa = null;
            switch(Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
            {
                case 0:
                    Console.WriteLine("Digite o nome da pessoa que deseja achar.");
                    do
                    {
                        nome = Console.ReadLine();
                    } while (nome == null);
                    pessoa = ServPessoa<Fisica>.Read(nome);
                    break;
                case 1:
                    Console.WriteLine("Digite o Id da pessoa que deseja achar.");
                    while(!ulong.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Clear();
                        Console.WriteLine("Precisa ser um numero inteiro positivo.");
                    }
                    pessoa = ServPessoa<Fisica>.Read(id);
                    break;
            }
            
            Console.Clear();
            if (pessoa == null) { Console.WriteLine("Pessoa não encontrada."); Console.ReadKey(true); }
            else
            {
                bool onLoop = true;
                bool removido = false;
                Console.WriteLine();
                do
                {
                    switch (Program.Menu("Pessoa encontrada, suas informações são:\n\n" +
                    $"{pessoa.Id}\t{pessoa.Nome}\t{pessoa.Email}\t{pessoa.DataNascimento.ToShortDateString()}\t{pessoa.Telefone}\t{pessoa.Cidade}\t{pessoa.Senha}\t{pessoa.Cpf}\t{pessoa.Pedidos}\n\n" +
                    "O que deseja fazer?", new string[] { "Editar", "Remover", "Mostrar Pedidos", "Voltar" }))
                    {
                        case 0:
                            Ademir.EditarFisica(pessoa);
                            break;
                        case 1:
                            Ademir.RemoverFisica(pessoa, out removido);
                            break;
                        case 2:
                            Ademir.MostrarPedidos(pessoa);
                            break;
                        default:
                            onLoop = false;
                            break;
                    }
                } while (onLoop && !removido);
            }
        }

        public static void AdicionarFisica()
        {
            string[] temp = new string[6];
            string[] temp2 = new string[]{ "Nome: ", "Email: ", "Telefone: ", "Cidade: ", "Senha: ", "Cpf: "};
            DateOnly DataNascimento;
            for (int i = 0; i < temp.Length; i++)
            {
                do
                {
                Console.Clear();
                Console.WriteLine("Para adicionar um usuário são necessárias algumas informações.");
                Console.Write(temp2[i]);
                temp[i] = Console.ReadLine();

                } while (temp[i] == "");
            }
            Console.Clear();
            Console.WriteLine("Para adicionar um usuário são necessárias algumas informações.");
            Console.Write("Data de Nascimento: ");
            while (!DateOnly.TryParse(Console.ReadLine(),out DataNascimento))
            {
                Console.Clear();
                Console.WriteLine("Precisa ser uma data.");
                Console.Write("Data de Nascimento: ");
            }

            Fisica p = new(temp[0], temp[1], DataNascimento, temp[2], temp[3], temp[4], temp[5]);
            if (ServPessoa<Fisica>.Add(p) == null) Console.WriteLine("Houve algum problema ao adicionar o novo usuário.");
            else Console.WriteLine("Usuário adicionado com sucesso.");
            Console.ReadKey(true);
        }

        public static void EditarFisica(Fisica pessoa)
        {
            Console.WriteLine("Para editar um campo escreva o novo valor, para deixar o antigo apenas deixe em branco.");

            string line;
            string?[] temp = new string[6];
            string[] temp2 = new string[] { "nome: ", "email: ", "telefone: ", "cidade: ", "senha: ", "cpf: " };
            string[] temp3 = new string[] { pessoa.Nome, pessoa.Email, pessoa.Telefone, pessoa.Cidade, pessoa.Senha, pessoa.Cpf };
            DateOnly DataNascimento;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + temp3[i]);
                Console.Write("Novo "+temp2[i]);
                temp[i] = Console.ReadLine();
            }
            do
            {
                Console.Clear();
                Console.WriteLine("Antigo valor: " + pessoa.DataNascimento.ToString());
                Console.Write("Nova data de nascimento: ");
                line = Console.ReadLine();
            } while (!DateOnly.TryParse(line, out DataNascimento) && line != null) ;
            Console.Clear();

            Fisica editado = new Fisica(temp[0] ?? pessoa.Nome, temp[1] ?? pessoa.Email, line == null ? pessoa.DataNascimento : DataNascimento, temp[2] ?? pessoa.Telefone, temp[3] ?? pessoa.Cidade, temp[4] ?? pessoa.Senha, temp[5]??pessoa.Senha);

            /////////DEBUGGING/////////////////////////////
            Console.WriteLine($"{editado.Id}\t{editado.Nome}\t{editado.Email}\t{editado.DataNascimento.ToShortDateString()}\t{editado.Telefone}\t{editado.Cidade}\t{editado.Senha}\t{editado.Cpf}\t{editado.Pedidos}\n\n");
            Console.ReadKey(true);
            /////////DEBUGGING/////////////////////////////

            ServPessoa<Fisica>.Edit(pessoa.Id, editado);
            
            if (editado == pessoa) Console.WriteLine("Informações atualizadas com sucesso.");
            else Console.WriteLine("Houve um erro ao atualizar as informações.");
            
            Console.ReadKey(true);

        }
    
        public static void RemoverFisica(Fisica pessoa, out bool removido)
        {
            switch(Program.Menu($"Tem certeza que deseja remover {pessoa.Nome}?", new string[] {"Sim", "Não"}))
            {
                case 0:
                    if (ServPessoa<Fisica>.Delete(pessoa.Id) == pessoa) Console.WriteLine("Usuário removido com sucesso.");
                    else Console.WriteLine("A remoção não pode ser concluida.");
                    Console.ReadKey(true);
                    break;
                default:
                    removido = false;
                    return;
            }
            removido = true;
        }

        public static void MostrarPedidos(Fisica pessoa)
        {

        }
    }
}

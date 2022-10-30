using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmJuridica
    {
        public static void Listar()
        {
            List<string> pessoas = new List<string>(ServPessoa<Juridica>.Browse().Count);
            foreach (Juridica pessoa in ServPessoa<Juridica>.Browse())
                pessoas.Add(
                    $"Id:{pessoa.Id}\t" +
                    $"Nome:{pessoa.Nome}");
            int index = Program.Menu("Lista de todas as pessoas, selecione uma para mais informações.", pessoas.ToArray());
            if (index == -1) return;
            Achar(ServPessoa<Juridica>.Browse()[index]);
        }

        public static void Achar(Juridica? pessoa = null)
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
                        Console.WriteLine("Digite o nome da empresa que deseja achar.");
                        nome = Console.ReadLine();
                        pessoa = ServPessoa<Juridica>.Read(nome);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Digite o Id da empresa que deseja achar.");
                            line = Console.ReadLine();
                        }
                        while (!ulong.TryParse(line, out id) && !string.IsNullOrEmpty(line));

                        pessoa = string.IsNullOrEmpty(line) ? null : ServPessoa<Juridica>.Read(id);
                        break;
                }
            }

            Console.Clear();
            if (pessoa == null) { Console.WriteLine("Empresa não encontrada."); Console.ReadKey(true); return; }

            bool onLoop = true;
            bool removido = false;
            Console.WriteLine();
            do
            {
                switch (Program.Menu("As informações da Empresa selecionada são:\n\n" +
                $"Id:\t{pessoa.Id}\n" +
                $"Nome:\t{pessoa.Nome}\n" +
                $"Email:\t{pessoa.Email}\n" +
                $"Inaug.:\t{pessoa.DataNascimento.ToShortDateString()}\n" +
                $"Tel.:\t{pessoa.Telefone}\n" +
                $"Cidade:\t{pessoa.Cidade}\n" +
                $"Senha:\t{pessoa.Senha}\n" +
                $"Cnpj:\t{pessoa.Cnpj}\n" +
                $"NumRes:\t{pessoa.Restaurantes}\n\n" +
                "O que deseja fazer?", new string[] { "Editar", "Remover", "Mostrar Restaurantes", "Adicionar Restaurante", "Remover Restaurante" }))
                {
                    case 0:
                        pessoa = AdmJuridica.Editar(pessoa);
                        break;
                    case 1:
                        AdmJuridica.Remover(pessoa, out removido);
                        break;
                    case 2:
                        AdmJuridica.MostrarRestaurantes(pessoa);
                        break;
                    case 3:
                        AdmJuridica.AdicionarRestaurante(pessoa);
                        break;
                    case 4:
                        AdmJuridica.RemoverRestaurante(pessoa, out removido);
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
            string[] temp = new string[] { "Nome: ", "Email: ", "Inauguração: ", "Telefone: ", "Cidade: ", "Senha: ", "Cnpj: " };
            DateOnly DataInauguração;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Para adicionar uma empresa são necessárias algumas informações.");
                Console.Write(temp[i]);
                temp[i] = Console.ReadLine();
                valido &= !string.IsNullOrEmpty(temp[i]);
            }
            valido &= DateOnly.TryParse(temp[2], out DataInauguração);

            Console.Clear();
            if (valido)
            {
                Juridica pessoa = new(temp[0], temp[1], DataInauguração, temp[3], temp[4], temp[5], temp[6]);
                if (ServPessoa<Juridica>.Add(pessoa) == pessoa)
                {
                    Console.WriteLine("Empresa adicionada com sucesso. Agora adicione um restaurante para a empresa.");
                    Console.ReadKey(true);
                    bool repeat = false;
                    do
                    {
                        AdicionarRestaurante(pessoa);

                        if (ServRestaurante.Browse(pessoa).Count == 0)
                            switch (Program.Menu("A empresa não tem restaurantes, deseja adicioná-los ou deletar a empresa?", new string[] { "Adicionar", "Deletar" }))
                            {
                                default:
                                    repeat = true;
                                    break;
                                case 1:
                                    repeat = false;
                                    ServPessoa<Juridica>.Delete(pessoa.Id);
                                    break;
                            }
                    } while (repeat);
                }
                else Console.WriteLine("Houve algum problema ao adicionar a nova empresa.");
            }
            else
            {
                Console.WriteLine("As informações dadas não são validas.");
                Console.ReadKey(true);
            }
        }

        private static Juridica Editar(Juridica pessoa)
        {
            Console.WriteLine("Para editar um campo escreva o novo valor, para deixar o antigo apenas deixe em branco.");

            string? line;
            string[] temp = new string[] { "nome: ", "email: ", "telefone: ", "cidade: ", "senha: ", "cpf: " };
            string[] temp2 = new string[] { pessoa.Nome, pessoa.Email, pessoa.Telefone, pessoa.Cidade, pessoa.Senha, pessoa.Cnpj };
            DateOnly DataNascimento;
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
                Console.WriteLine("Antigo valor: " + pessoa.DataNascimento.ToString());
                Console.Write("Nova data de nascimento: ");
                line = Console.ReadLine();
            } while (!DateOnly.TryParse(line, out DataNascimento) && !string.IsNullOrEmpty(line));
            Console.Clear();

            Juridica editado = new Juridica(
                string.IsNullOrEmpty(temp[0]) ? pessoa.Nome : temp[0],
                string.IsNullOrEmpty(temp[1]) ? pessoa.Email : temp[1],
                string.IsNullOrEmpty(line) ? pessoa.DataNascimento : DataNascimento,
                string.IsNullOrEmpty(temp[2]) ? pessoa.Telefone : temp[2],
                string.IsNullOrEmpty(temp[3]) ? pessoa.Cidade : temp[3],
                string.IsNullOrEmpty(temp[4]) ? pessoa.Senha : temp[4],
                string.IsNullOrEmpty(temp[5]) ? pessoa.Cnpj : temp[5]);

            ServPessoa<Juridica>.Edit(pessoa.Id, editado);
            pessoa = ServPessoa<Juridica>.Read(pessoa.Id);

            if (pessoa == editado) Console.WriteLine("Informações atualizadas com sucesso.");
            else Console.WriteLine("Houve um erro ao atualizar as informações.");

            Console.ReadKey(true);
            return pessoa;
        }

        private static void Remover(Juridica pessoa, out bool removido)
        {
            switch (Program.Menu($"Tem certeza que deseja remover {pessoa.Nome}?", new string[] { "Não", "Sim" }))
            {
                case 1:
                    if (ServPessoa<Juridica>.Delete(pessoa.Id) == pessoa) Console.WriteLine("Usuário removido com sucesso.");
                    else Console.WriteLine("A remoção não pode ser concluida.");
                    Console.ReadKey(true);
                    break;
                default:
                    removido = false;
                    return;
            }
            removido = true;
        }

        private static void MostrarRestaurantes(Juridica pessoa)
        {
            if (ServRestaurante.Browse(pessoa).Count == 0) Console.WriteLine("Nenhum Restaurante.");
            foreach (Restaurante restaurante in ServRestaurante.Browse(pessoa))
            {
                Console.WriteLine(
                    $"Id:\t\t{restaurante.Id}\n" +
                    $"Nome:\t\t{restaurante.Nome}\n" +
                    $"Endereço:\t{restaurante.Endereco}\n" +
                    $"Descricao:\t{restaurante.Descricao}\n" +
                    $"Cardápio:");
                foreach (Item item in ServItem.Browse(restaurante))
                {
                    if (item != null)
                        Console.WriteLine($"\tId:{item.Id}\t" +
                            $"Nome:{item.Nome}\t" +
                            $"Descr:{item.Descricao}\t" +
                            $"R${item.Valor}");
                    else Console.WriteLine("Item não encontrado");
                }
            }
            Console.ReadKey(true);
        }

        private static void AdicionarRestaurante(Juridica pessoa)
        {
            bool valido = true;
            string[] temp = new string[] { "Nome: ", "Endereço: ", "Descrição: "};
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Para adicionar um restaurante são necessárias algumas informações.");
                Console.Write(temp[i]);
                temp[i] = Console.ReadLine();
                valido &= !string.IsNullOrEmpty(temp[i]);
            }

            Console.Clear();
            if (valido)
            {

                Restaurante restaurante = new(pessoa, temp[0], temp[1], temp[2]);
                if (ServRestaurante.Add(restaurante) == restaurante)
                {
                    Console.WriteLine("Restaurante adicionado com sucesso. Agora o restaurante precisa de um cardapio.");
                    Console.ReadKey(true);
                    bool repeat = false;
                    do
                    {
                        AdmRestaurante.AdicionarItem(restaurante);
                        if (ServItem.Browse(restaurante).Count == 0)
                            switch (Program.Menu("O restaurante não tem itens, deseja adicioná-los ou deletar o restaurante?", new string[] { "Adicionar", "Deletar" }))
                            {
                                default:
                                    repeat = true;
                                    break;
                                case 1:
                                    repeat = false;
                                    ServRestaurante.Delete(restaurante.Id);
                                    break;
                            }
                    } while (repeat);

                }
                else Console.WriteLine("Houve algum problema ao adicionar o novo restaurante.");
            }
            else
            {
                Console.WriteLine("As informações dadas não são validas.");
                Console.ReadKey(true);
            }
        }
        private static void RemoverRestaurante(Juridica pessoa, out bool removido)
        {
            List<string> temp = new List<string>(ServRestaurante.Browse(pessoa).Count);
            string entry;
            foreach (Restaurante restaurante in ServRestaurante.Browse(pessoa))
            {
                entry =
                    $"Id:\t\t{restaurante.Id}\n" +
                    $"Nome:\t{restaurante.Nome}\n" +
                    $"Endereço:\t{restaurante.Endereco}\n" +
                    $"Cardápio:\n";
                foreach (Item item in ServItem.Browse(restaurante))
                {
                    entry +=
                        $"\tId:{item.Id}\t" +
                        $"Nome:{item.Nome}\t" +
                        $"Descrição:{item.Descricao}\t" +
                        $"Valor:{item.Valor}\t" +
                        $"Imagem:{item.Imagem}";
                }
                temp.Add(entry);
            }
            bool repeat = false;
            do
            {
                int resposta = Program.Menu("Selecione o restaurante a ser deletado", temp.ToArray());
                if (resposta == -1) { removido = false; return; }
                Restaurante selecionado = ServRestaurante.Browse()[resposta];

                switch (Program.Menu($"Tem certeza que deseja deletar {selecionado.Nome}?", new string[] { "Não", "Sim" }))
                {
                    case 1:
                        repeat = false;
                        if (ServRestaurante.Delete(selecionado.Id) == selecionado) Console.WriteLine("Restaurante deletado com sucesso.");
                        else Console.WriteLine("Houve um erro ao deletar o restaurante.");
                        Console.ReadKey(true);

                        if(ServRestaurante.Browse(pessoa).Count == 0) 
                            switch(Program.Menu("Esse era o ultimo restaurante da empresa, deseja adicionar outro ou deletar a empresa?", new string[] {"Adicionar", "Deletar"}))
                            {
                                default:
                                    AdicionarRestaurante(pessoa);
                                    break;
                                case 1:
                                    removido = true;
                                    ServPessoa<Juridica>.Delete(pessoa.Id);
                                    return;
                            }
                        break;
                    default:
                        repeat = true;
                        break;
                }
            } while (repeat);
            removido = false;
        }
    }
}

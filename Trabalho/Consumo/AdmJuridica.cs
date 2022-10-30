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
            string[] pessoas = new string[ServPessoa<Juridica>.Browse().Count + 2];
            pessoas[0] = "Voltar";
            for (int i = 0; i < ServPessoa<Juridica>.Browse().Count; i++)
                pessoas[i + 1] =
                    $"Id: {ServPessoa<Juridica>.Browse()[i].Id}\t" +
                    $"Nome: {ServPessoa<Juridica>.Browse()[i].Nome}\t" +
                    $"Restaurantes: {ServPessoa<Juridica>.Browse()[i].Restaurantes}";
            pessoas[ServPessoa<Juridica>.Browse().Count + 1] = "Voltar";
            int index = Program.Menu("Lista de todas as empresas, selecione uma para mais informações.", pessoas);
            if (index == ServPessoa<Juridica>.Browse().Count + 1 || index == 0) return;
            Achar(ServPessoa<Juridica>.Browse()[index - 1]);
        }

        public static void Achar(Juridica? pessoa = null)
        {
            if (pessoa == null)
            {
                ulong id;
                string? nome, line;
                switch (Program.Menu("Procurar por nome ou Id?", new string[] { "Nome", "Id" }))
                {
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
            if (pessoa == null) { Console.WriteLine("Empresa não encontrada."); Console.ReadKey(true); }
            else
            {
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
                    "O que deseja fazer?", new string[] { "Editar", "Remover", "Mostrar Restaurantes", "Adicionar Restaurante", "Voltar" }))
                    {
                        case 0:
                            //pessoa = AdmJuridica.Editar(pessoa);
                            break;
                        case 1:
                            //AdmJuridica.Remover(pessoa, out removido);
                            break;
                        case 2:
                            //AdmJuridica.MostrarRestaurantes(pessoa);
                            break;
                        case 3:
                            //AdmJuridica.AdicionarRestaurante(pessoa);
                            break;
                        default:
                            onLoop = false;
                            break;
                    }
                } while (onLoop && !removido);
            }
        }
        ///////////////////////////////////////////////
        //AINDA NÃO TESTADO, NÃO VAI FUNCIONAR/////////
        ///////////////////////////////////////////////
        /*public static void Adicionar()
        { 
            bool valido = true;
            string[] temp = new string[] { "Nome: ", "Email: ", "Inauguração: ", "Telefone: ", "Cidade: ", "Senha: ", "Cnpj: " };
            DateOnly DataNascimento;
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Para adicionar uma empresa são necessárias algumas informações.");
                Console.Write(temp[i]);
                temp[i] = Console.ReadLine();
                if (string.IsNullOrEmpty(temp[i])) valido = false;
            }
            valido &= DateOnly.TryParse(temp[2], out DataNascimento);

            string[] temp2 = new string[] { "Nome: ", "Endereço: ", "Descriçao: " };
            for(int i = 0; i < temp.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Para cadastrar uma empresa ela deve ter um restaurante. Coloque as informações referente a ele.");
                Console.Write(temp2[i]);
                if(string.IsNullOrEmpty(temp2[i])) valido = false;
            }

            string[] temp3 = new string[] { "Nome: ", "Descrição: ", "Valor: ", "Imagem: " };
            for(int i = 0; i < temp3.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Um restaurante precisa de um cardápio, para o primeiro restaurante será necessário criar um novo cardápio.");
                switch(Program.Menu("Podem ser escolhidos itens já existentes de outros restaurantes ou podem ser criados novos.", new string[] { "Escolher existentes", "Criar Novos" }))
                {
                }
            }
            Console.Clear();
            if (valido)
            {
                Juridica p = new(temp[0], temp[1], DataNascimento, temp[3], temp[4], temp[5], temp[6]);
                if (ServPessoa<Juridica>.Add(p) == p) Console.WriteLine("Usuário adicionado com sucesso.");
                else Console.WriteLine("Houve algum problema ao adicionar o novo usuário.");
            }
            else Console.WriteLine("As informações dadas não são validas.");
            Console.ReadKey(true);
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
            switch (Program.Menu($"Tem certeza que deseja remover {pessoa.Nome}?", new string[] { "Sim", "Não" }))
            {
                case 0:
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
            foreach (Restaurante Restaurante in ServRestaurante.Browse(pessoa))
            {
                Console.WriteLine($"Id do Restaurante: {Restaurante.Id}");
                foreach (ulong id in Restaurante.Cardapio)
                {
                    Item? item = ServItem.Read(id);
                    if (item != null)
                        Console.WriteLine($"\tId:{item.Id}\tNome:{item.Nome}\tDescr:{item.Descricao}\tR${item.Valor}");
                    else Console.WriteLine("Item não encontrado");
                }
            }
            Console.ReadKey(true);
        }

        private static void AdicionarRestaurante(Juridica pessoa)
        {
            List<Item> itens = new List<Item>();
            bool? terminado = false;
            do
            {

                float total = 0;
                string line = itens.Count == 0 ? "Nenhum item adicionado." : "";
                foreach (Item item in itens)
                {
                    line += $"{item.Nome}, R${item.Valor:0.##}\n";
                    total += item.Valor;
                }
                switch (Program.Menu(line + "\nTotal = R$" + total.ToString("0.##") + "\nSelecione o que deseja fazer.", new string[] { "Adicionar item", "Remover item", "Terminar Restaurante", "Cancelar" }))
                {
                    case 0:
                        string[] restaurantes = new string[ServRestaurante.Browse().Count];
                        for (int i = 0; i < ServRestaurante.Browse().Count; i++) restaurantes[i] = ServRestaurante.Browse()[i].Nome;

                        Restaurante escolhido = ServRestaurante.Browse()[Program.Menu("Escolha o restaurante para mostrar os itens.", restaurantes)];

                        string[] itensString = new string[ServItem.Browse(escolhido).Count];
                        for (int i = 0; i < ServItem.Browse(escolhido).Count; i++)
                            itensString[i] =
                                $"Nome:{ServItem.Browse(escolhido)[i].Nome}\t" +
                                $"Descr:{ServItem.Browse(escolhido)[i].Descricao}\t" +
                                $"R${ServItem.Browse(escolhido)[i].Valor}\t" +
                                $"Imagem:{ServItem.Browse(escolhido)[i].Imagem}";

                        itens.Add(ServItem.Browse(escolhido)[Program.Menu("Selecione o item para adicionar no Restaurante", itensString)]);

                        break;
                    case 1:
                        string[] itensS = new string[itens.Count];
                        for (int i = 0; i < itens.Count; i++) itensS[i] = $"{itens[i].Nome}, {itens[i].Valor}";
                        itens.RemoveAt(Program.Menu("Escolha o item a ser removido.", itensS));
                        break;
                    case 2:
                        if (itens.Count == 0) terminado = Program.Menu("A lista está vazia, deseja cancelar o Restaurante?", new string[] { "Sim", "Não" }) == 0 ? null : false;
                        else terminado = Program.Menu("Tem certeza de que deseja terminar o Restaurante?", new string[] { "Sim", "Não" }) == 0 ? true : false;
                        break;
                    default:
                        terminado = Program.Menu("Tem certeza de que deseja cancelar o Restaurante?", new string[] { "Sim", "Não" }) == 0 ? null : false;
                        break;
                }
            } while (terminado == false);

            if (terminado == true)
            {
                Restaurante p = new Restaurante(pessoa, itens);
                if (ServRestaurante.Add(p) == p) Console.WriteLine("Restaurante criado com sucesso.");
                else Console.WriteLine("Houve um erro ao concluir o Restaurante.");
            }
            else Console.WriteLine("Restaurante cancelado com sucesso.");

            Console.ReadKey(true);
        }*/
    }
}

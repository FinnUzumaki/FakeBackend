using System;
using Trabalho.Consumo;


class Program
{
    public static void Main(string[] args)
    {
        
        //cria instancias de teste
        Ademir.Popular();

        while(true)
        {
            bool onSecond = false;
            switch(Menu("O que deseja ver?", new string[] { "Pessoas Fisicas", "Pedidos", "Pessoas Juridicas", "Restaurantes", "Itens", "Sair" }))
            {
                case 0:
                    onSecond = true;
                    do
                    {
                        switch (Menu("Menu de Pessoas Fisicas, o que deseja fazer?", new string[] { "Listar", "Achar", "Adicionar", "Voltar" }))
                        {
                            case 0:
                                Ademir.ListarFisica();
                                break;
                            case 1:
                                Ademir.AcharFisica();
                                break;
                            case 2:
                                Ademir.AdicionarFisica();
                                break;
                            default:
                                onSecond = false;
                                break;
                        }

                    } while (onSecond);
                    break;
                case 1:
                    onSecond = true;
                    do
                    {
                        switch (Menu("Menu de Pedidos, o que deseja fazer?", new string[] { "Listar", "Achar", "Adicionar", "Remover", "Editar", "Voltar" }))
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                onSecond = false;
                                break;
                        }

                    } while (onSecond);
                    break;
                case 2:
                    onSecond = true;
                    do
                    {
                        switch (Menu("Menu de Pessoas Juridicas, o que deseja fazer?", new string[] { "Listar", "Achar", "Adicionar", "Remover", "Editar", "Voltar" }))
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                onSecond = false;
                                break;
                        }

                    } while (onSecond);
                    break;
                case 3:
                    onSecond = true;
                    do
                    {
                        switch (Menu("Menu de Restaurantes, o que deseja fazer?", new string[] { "Listar", "Achar", "Adicionar", "Remover", "Editar", "Voltar" }))
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                onSecond = false;
                                break;
                        }

                    } while (onSecond);
                    break;
                case 4:
                    onSecond = true;
                    do
                    {
                        switch (Menu("Menu de Itens, o que deseja fazer?", new string[] { "Listar", "Achar", "Adicionar", "Remover", "Editar", "Voltar" }))
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                onSecond = false;
                                break;
                        }

                    } while (onSecond);
                    break;
                default:
                    return;
            }
        }
    }
    public static int Menu(string msg, string[] opcoes)
    {
        ConsoleKey chave = 0;
        int selecionada = 0;
        do
        {
            Console.Clear();
            Console.WriteLine(msg);

            if (chave == ConsoleKey.UpArrow || chave == ConsoleKey.W) selecionada--;
            else if (chave == ConsoleKey.DownArrow || chave == ConsoleKey.S) selecionada++;

            if (selecionada < 0) selecionada = opcoes.Length - 1;
            else if (selecionada > opcoes.Length - 1) selecionada = 0;

            for (int i = 0; i < opcoes.Length; i++)
            {
                Console.WriteLine(opcoes[i] + (selecionada == i ? "<" : ""));
            }

            chave = Console.ReadKey(true).Key;
        } while (chave != ConsoleKey.Enter);

        Console.Clear();
        return selecionada;
    }
}
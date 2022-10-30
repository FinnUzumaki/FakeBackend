using Trabalho.Dominio;
using Trabalho.Servico;

namespace Trabalho.Consumo
{
    public static class AdmRestaurante
    {
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

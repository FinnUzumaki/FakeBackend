using Trabalho.Dominio;
using Trabalho.Servico;

for(int i = 0; i < 10; i++)
{
    Fisica nova = new Fisica("Fisica Nome " + i, "Fisica Email " + i, new DateOnly(2002, 12, 20), "Fisica telefone " + i, "Fisica cidade " + i, "Fisica senha " + i, "Fisica cpf " + i);
    ServPessoa<Fisica>.Add(nova);
    ServPedido.Add(nova, new Pedido());
}


Console.WriteLine(ServPessoa<Fisica>.Read(5).Nome);
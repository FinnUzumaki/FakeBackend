using Trabalho.Dominio;
using Trabalho.Servico;

for(int i = 0; i < 10; i++)
{
    Fisica nova = new Fisica("Fisica Nome " + i, "Fisica Email " + i, new DateOnly(2002, 12, 20), "Fisica telefone " + i, "Fisica cidade " + i, "Fisica senha " + i, "Fisica cpf " + i);
    ServPessoa<Fisica>.Add(nova);
    ServPedido.Add(new Pedido(nova.Id));
}


Console.WriteLine(ServPedido.Read(5)?.IdPessoa);
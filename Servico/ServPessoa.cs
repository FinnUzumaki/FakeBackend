using Trabalho.Dominio;
using Trabalho.Repositorio;

namespace Trabalho.Servico
{
    public static class ServPessoa<TPessoa> where TPessoa : BasePessoa
    {
        public static List<TPessoa> Browse()
        {
            return RepoPessoa<TPessoa>.ReadAll();
        }
        public static TPessoa? Read(long id)
        {
            return RepoPessoa<TPessoa>.Read(id);
        }
        public static TPessoa? Edit(long id, TPessoa instancia)
        {
            return RepoPessoa<TPessoa>.Update(id, instancia);
        }
        public static TPessoa Add(TPessoa instancia)
        {
            return RepoPessoa<TPessoa>.Create(instancia);
        }
        public static TPessoa? Delete(long id)
        {
            return RepoPessoa<TPessoa>.Delete(id);
        }
    }
}
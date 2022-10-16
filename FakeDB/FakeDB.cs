using Trabalho.Dominio;

namespace Trabalho.FakeDB
{
    public static class FakeDB<TDominio> where TDominio : class
    {
        private static List<TDominio> lista;

        public static List<TDominio> Lista
        {
            get
            {
                if (lista == null) lista = new List<TDominio>();
                return lista;
            }
        }
    }
}
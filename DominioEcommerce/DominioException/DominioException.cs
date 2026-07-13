using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.DominioException
{
    public class DominioException : Exception
    {
        public List<string> Erros { get; }

        public DominioException(string mensagem, List<string> erros) : base(mensagem)
        {
            Erros = erros;
        }
    }
}

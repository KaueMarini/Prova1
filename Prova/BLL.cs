using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova
{
    class BLL
    {
        public static void conecta()
        {
            DAL.conecta();
        }

        public static void desconecta()
        {
            DAL.desconecta();
        }
        public static bool getCliente(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                Erro.setMsg("O CNPJ é de preenchimento obrigatório!");
                return false;
            }

            return DAL.consultaUmCliente(cnpj);
        }
        public static List<string[]> getVendasCliente(string cnpj)
        {
            return DAL.consultaVendasCliente(cnpj);
        }
    }
}

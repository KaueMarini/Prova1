using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Prova
{
    class DAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDFarinha.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }

        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static bool consultaUmCliente(string cnpj)
        {
            try
            {
                string sql = "SELECT * FROM Clientes WHERE CNPJ = @cnpj";
                strSQL = new OleDbCommand(sql, conn);
                strSQL.Parameters.AddWithValue("@cnpj", cnpj);
                result = strSQL.ExecuteReader();

                if (result.Read())
                {
                    Cliente.setCNPJ(result["CNPJ"].ToString());
                    Cliente.setNome(result["Nome"].ToString());
                    result.Close();
                    return true;
                }
                result.Close();
                return false;
            }
            catch (Exception ex)
            {
                Erro.setMsg("Erro ao consultar cliente: " + ex.Message);
                return false;
            }
        }

        public static List<string[]> consultaVendasCliente(string cnpj)
        {
            List<string[]> vendas = new List<string[]>();
            try
            {
                string sql = "SELECT * FROM VendasCliente WHERE CNPJ = @cnpj";
                strSQL = new OleDbCommand(sql, conn);
                strSQL.Parameters.AddWithValue("@cnpj", cnpj);
                result = strSQL.ExecuteReader();

                while (result.Read())
                {
                    string data = Convert.ToDateTime(result["DataVenda"]).ToShortDateString();
                    string toneladas = result["Toneladas"].ToString();
                    string valor = result["Valor"].ToString();

                    vendas.Add(new string[] { data, toneladas, valor });
                }
                result.Close();
            }
            catch (Exception ex)
            {
                Erro.setMsg("Erro ao consultar vendas: " + ex.Message);
            }
            return vendas;
        }


    }
}

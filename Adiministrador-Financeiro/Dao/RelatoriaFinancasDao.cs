using Adiministrador.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Dao
{
    class RelatoriaFinancasDao
    {
        public List<RelatorioFinancasModel> rela(DateTime inicio, DateTime fim, int idSelecao)/// id 0 padrão para nem uma seleçaõ de entrada ou saida
        {

            string auxInicio = inicio.ToString("yyyy-MM-dd");
            string auxFim = fim.ToString("yyyy-MM-dd");
            string query = "SELECT C.Name AS Conta, F.Valor, F.EntradaSaida, F.Data AS Date, F.Origem FROM" +
                " FinancasModel AS F " +
                " LEFT JOIN ContasModel AS C ON F.IdConta = C.Id " +
                "WHERE F.Data BETWEEN '" +
                 auxInicio + "' AND '" + auxFim + "'";
            if (idSelecao > 0)
            {///Idselecao  1 é para entradas e 2 para saidas
                if (idSelecao == 1)
                {
                    query += " AND F.EntradaSaida = 'E'";
                }
                else
                {
                    query += " AND F.EntradaSaida = 'S' OR F.EntradaSaida = 'N'";
                }
            }
            query += " ORDER BY F.Data";

            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            var Lista = db.Query<RelatorioFinancasModel>(query);
            return Lista;
        }
    }
}
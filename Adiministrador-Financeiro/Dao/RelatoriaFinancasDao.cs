using Adiministrador.Model;
using Adiministrador_Financeiro.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador_Financeiro.Dao
{
     class RelatoriaFinancasDao
    {
        public List<RelatorioFinancasModel> rela(DateTime inicio, DateTime fim, int veicolo)///veicolo id 0 padrão para nem um veicolo selecionado
        {
            string auxInicio = inicio.ToString("yyyy-MM-dd");
            string auxFim = fim.ToString("yyyy-MM-dd");
            string query = "SELECT V.Name AS Veicolo, P.Name AS Posto, A.LitrosTotal, A.ValorLitro, A.Date, A.kmPercorido, A.Hodometro FROM" +
                " AbastecimentoModel AS A " +
                " JOIN PostoModel AS P ON A.Posto = P.Id " +
                "JOIN VeicoloModel AS V ON A.Veicolo = V.Id " +
                "WHERE A.Date BETWEEN '" +
                 auxInicio + "' AND '" + auxFim + "'";

            if (veicolo > 0)
            {
                query += " AND A.Veicolo = " + veicolo;
            }
            query += " ORDER BY A.Date";

            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            var Lista = db.Query<RelatorioFinancasModel>(query);
            return Lista;

        }

    }
}

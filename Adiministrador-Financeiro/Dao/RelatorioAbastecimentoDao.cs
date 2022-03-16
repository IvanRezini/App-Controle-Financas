using Adiministrador.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adiministrador.Dao
{
    internal class RelatorioAbastecimentoDao
    {
        public List<RelatorioAbastecimentoModel> rela()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            var Lista = db.Query<RelatorioAbastecimentoModel>
                ("SELECT P.Name  AS Posto, A.LitrosTotal, A.ValorLitro, A.Date, A.kmPercorido, A.Hodometro FROM AbastecimentoModel AS A  JOIN PostoModel AS P ON A.Posto = P.Id");//WHERE Symbol = ?", "A"
           
            return Lista;

        }

    }
}
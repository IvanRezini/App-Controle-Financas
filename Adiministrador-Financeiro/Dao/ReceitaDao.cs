using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adiministrador.Dao
{
    internal class ReceitaDao
    {
        public List<ReceitaModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<ReceitaModel>()
                    select data).ToList();
        }
        private ReceitaModel GeID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<ReceitaModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

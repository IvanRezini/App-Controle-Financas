using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Adiministrador.Dao
{
    internal class AbastcimentoDao
    {
        public List<AbastecimentoModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<AbastecimentoModel>()
                    select data).ToList();
        }
        private AbastecimentoModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<AbastecimentoModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

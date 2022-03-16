using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Adiministrador.Dao
{
    internal class SaqueDao
    {
        public List<SaquesModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<SaquesModel>()
                    select data).ToList();
        }
        private SaquesModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<SaquesModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

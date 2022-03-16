using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adiministrador.Dao
{
    internal class BancoDao
    {
        public List<BancoModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<BancoModel>()
                    select data).ToList();
        }
        private BancoModel GeID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<BancoModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

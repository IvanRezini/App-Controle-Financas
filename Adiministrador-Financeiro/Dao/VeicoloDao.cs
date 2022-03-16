using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Adiministrador.Dao
{
    internal class VeicoloDao
    {
        public List<VeicoloModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<VeicoloModel>()
                    select data).ToList();
        }
        public VeicoloModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<VeicoloModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

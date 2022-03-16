using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Adiministrador.Dao
{
    internal class ContaDao
    {
        public List<ContasModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<ContasModel>()
                    select data).ToList();
        }
        private ContasModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<ContasModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

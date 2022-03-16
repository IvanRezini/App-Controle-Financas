using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adiministrador.Dao
{
    internal class ClienteDao
    {
        public List<ClienteModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<ClienteModel>()
                    select data).ToList();
        }
        public ClienteModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<ClienteModel>().FirstOrDefault(t => t.Id == id);
        }
    }
}

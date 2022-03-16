using System;
using Adiministrador.Model;
using System.Linq;
using System.Collections.Generic;

namespace Adiministrador.Dao
{
    class PostoDao
    {
        
        public List<PostoModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<PostoModel>()
                    select data).ToList();
        }
        public PostoModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<PostoModel>().FirstOrDefault(t => t.Id == id);
        }
        public PostoModel GetName(string name)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<PostoModel>().FirstOrDefault(t => t.Name == name);
        }

    }
}
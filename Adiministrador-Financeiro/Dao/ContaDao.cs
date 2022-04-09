using System;
using Adiministrador.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Adiministrador.Dao
{
     class ContaDao
    {
        public List<ContasModel> Get()
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return (from data in db.Table<ContasModel>()
                    select data).ToList();
        }
        public ContasModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<ContasModel>().FirstOrDefault(t => t.Id == id);
        }
        public void CaregarListaBanco()
        {
            Contexto contexto = new Contexto();
            ContasModel bb = new ContasModel();

            bb.Name = "Abastecimento";
            contexto.insert(bb); ;

            bb.Name = "Luz";
            contexto.insert(bb);

            bb.Name = "Condominio";
            contexto.insert(bb);

            bb.Name = "Agua";
            contexto.insert(bb);

            bb.Name = "Internet";
            contexto.insert(bb);

        }
    }
}

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
        public BancoModel GetID(int id)
        {
            Contexto contexto = new Contexto();
            var db = contexto.conexao;
            return db.Table<BancoModel>().FirstOrDefault(t => t.Id == id);
        }
        /* Ao iniciar 
         * Inseri Alguns nomes de banco
         * 
         */
        public void CaregarListaBanco()
        {
            Contexto contexto = new Contexto();
            BancoModel bb = new BancoModel();
               
                bb.Name = "Banco Do Brasil";
            contexto.insert(bb); ;
              
                bb.Name = "Bradesco";
            contexto.insert(bb);

            bb.Name = "Itau";
            contexto.insert(bb);

            bb.Name = "Santander";
                contexto.insert(bb);

            bb.Name = "Caixa Economica";
            contexto.insert(bb);

        }
    }
}

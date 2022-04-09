using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adiministrador.Model
{
    public class Contexto
    {
        public SQLiteConnection conexao;

        public Contexto()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("appcadastro", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            conexao = new SQLiteConnection(arquivo.Path);
            conexao.CreateTable<ClienteModel>();
            conexao.CreateTable<FinancasModel>();
            conexao.CreateTable<PostoModel>();
            conexao.CreateTable<VeicoloModel>();
            conexao.CreateTable<AbastecimentoModel>();
            conexao.CreateTable<ContasModel>();
           

        }
        public void insert<T>(T modelo)
        {
            conexao.Insert(modelo);
        }
        public void Update<T>(T modelo)
        {
            conexao.Update(modelo);
        }
        public void Delete<T>(T modelo)
        {
            conexao.Delete(modelo);
        }
     
        
        
    }
}
//http://www.macoratti.net/18/11/xf_sqlitemvvm1.htm     Lista de cruds
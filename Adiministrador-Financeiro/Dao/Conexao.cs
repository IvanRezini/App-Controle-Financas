using Adiministrador.Model;
using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;


namespace Adiministrador.Dao
{
    class Conexao
    {
         static SQLiteConnection conexao;

        public static SQLiteConnection conexaoBanco()
        {
            string dbPath = Path.Combine(
         Environment.GetFolderPath(Environment.SpecialFolder.Personal),
         "appcadastro.db");
            conexao = new SQLiteConnection(dbPath);
            return conexao;
        }

    }
}
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    class ContasModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdConta {  get; set; }
        public int IdBanco { get; set; }
        public string Data {  get; set; }
        public string Valor {  get; set; }
    }
}

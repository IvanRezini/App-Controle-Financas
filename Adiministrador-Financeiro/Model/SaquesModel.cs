using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    class SaquesModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdBanco { get; set; }
        public DateTime Data { get; set; }
        public string Valor { get; set; }
    }
}

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
        public string Name {  get; set; }
    }
}

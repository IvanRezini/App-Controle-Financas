using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    class VeicoloModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Hodometro { get; set; }
    }
}

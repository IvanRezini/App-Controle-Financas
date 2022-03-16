using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    class AbastecimentoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Veicolo { get; set; }
        public int Posto {  get; set; }
        public string LitrosTotal { get; set; }
        public string ValorLitro { get; set; }
        public DateTime Date { get; set; }
        public string kmPercorido {  get; set;}
        public string Hodometro { get; set; }
    }
}

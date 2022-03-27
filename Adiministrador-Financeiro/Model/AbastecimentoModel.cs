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
        public string Date { get; set; }
        public string kmPercorido {  get; set;}
        public string Hodometro { get; set; }
        public int FormaPagamento { get; set; }/// 1 para dinheiro 2 para cartao e pix
    }
}

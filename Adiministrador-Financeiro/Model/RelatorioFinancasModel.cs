using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador_Financeiro.Model
{
     class RelatorioFinancasModel
    {
        public string Date { get; set; }
        public string Conta { get; set; }
        public string Valor { get; set; }
        public string Cor { get; set; }///cor da linha do relatorio
    }
}

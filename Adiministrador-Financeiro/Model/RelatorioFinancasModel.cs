using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
     class RelatorioFinancasModel
    {
        public string Date { get; set; }
        public string Conta { get; set; } //vem de outra tabela
        public string Valor { get; set; }
        public string EntradaSaida { get; set; }//E entrada S saida N para pagamento em dinheiro no qual ja foi sacado
        public int Origem { get; set; }//1 - Salario", "2 - Extra", "3 - Doação", "4 - Outro"
        public string CorLinha { get; set; }///cor da linha do relatorio
        public string CorTexto { get; set; }///cor da texto do relatorio
    }
}
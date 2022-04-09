using SQLite;

namespace Adiministrador.Model
{
  class FinancasModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdConta { get; set; }// id em branco significa um saque
        public int Origem { get; set; }//1 - Salario", "2 - Extra", "3 - Doação", "4 - Outro"
        public string Data { get; set; }
        public string Valor { get; set; }
        public string EntradaSaida { get; set; }//E entrada S saida N para pagamento em dinheiro no qual ja foi sacado
    }
}

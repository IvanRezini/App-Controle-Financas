using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    class ReceitaModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Valor {  get; set; }
        public DateTime Data {  get; set; } 
        public int Origem {  get; set; }    //"1 - Salario","2 - Extra","3 - Doação", "4 - Outro"
}
}

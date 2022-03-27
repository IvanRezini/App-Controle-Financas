using Adiministrador.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador_Financeiro.Controller
{
    internal class Inicialicacao
    {
        public void Iniciar()
        {
            BancoDao bb = new BancoDao();
            var resposta = bb.GetID(1);
            if (resposta == null)
            {
                bb.CaregarListaBanco();
            }
        }
    }
}

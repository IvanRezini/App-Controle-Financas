using Adiministrador.Dao;
using Adiministrador.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Controller
{
    internal class RelatorioAbastecimento
    {
        public List<RelatorioAbastecimentoModel> Relatorio(DateTime inicio, DateTime fim, int veicolo)
        {
            RelatorioAbastecimentoDao rd = new RelatorioAbastecimentoDao();
            List<RelatorioAbastecimentoModel>rm  = rd.rela(inicio,fim,veicolo);

            RelatorioAbastecimentoModel aux = new RelatorioAbastecimentoModel();
            
            List<RelatorioAbastecimentoModel> relatorioFinal = new List<RelatorioAbastecimentoModel>();///montagem relatorio

            foreach (var s in rm)
            {
                aux = new RelatorioAbastecimentoModel();
                aux.Data = s.Date;
                aux.ValorLitro = s.ValorLitro;
                aux.LitrosTotal = s.LitrosTotal;
                aux.ValorTotal = ((decimal.Parse(s.ValorLitro)) * (decimal.Parse(s.LitrosTotal))).ToString("N3");
                aux.Media = ((decimal.Parse(s.kmPercorido)) / (decimal.Parse(s.LitrosTotal))).ToString("N3");
                aux.kmPercorido = s.kmPercorido;
                aux.Posto = s.Posto; 
                relatorioFinal.Add(aux);
            }
            return relatorioFinal;

        }
    }
}

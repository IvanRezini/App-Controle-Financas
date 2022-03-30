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
            
            decimal tG = 0;
            decimal tL = 0;
            decimal tK= 0;
            int auxCor= 0;  
            foreach (var s in rm)
            {
                aux = new RelatorioAbastecimentoModel();
                if (auxCor == 0) {
                    aux.Cor = "Azure";
                    auxCor = 1;
                } else {
                    aux.Cor = "LightGreen";
                    auxCor = 0 ;
                }
                aux.Veicolo = s.Veicolo;
                aux.Data = s.Date;
                aux.ValorLitro = s.ValorLitro;
                aux.LitrosTotal = s.LitrosTotal;
                aux.ValorTotal = ((decimal.Parse(s.ValorLitro)) * (decimal.Parse(s.LitrosTotal))).ToString("N3");
                aux.Media = ((decimal.Parse(s.kmPercorido)) / (decimal.Parse(s.LitrosTotal))).ToString("N3");
                aux.kmPercorido = s.kmPercorido;
                aux.Posto = s.Posto;
                tL += (decimal.Parse(s.LitrosTotal));
                tG += ((decimal.Parse(s.ValorLitro)) * (decimal.Parse(s.LitrosTotal)));
                tK += (decimal.Parse(s.kmPercorido));
                relatorioFinal.Add(aux);
            }
            aux = new RelatorioAbastecimentoModel();
            aux.Cor = "White";
            relatorioFinal.Add(aux);

            aux = new RelatorioAbastecimentoModel();
            aux.Data = "Toatal L:";
            aux.Cor = "Lime";
            aux.ValorLitro = tL.ToString("N2");
            relatorioFinal.Add(aux);
            aux = new RelatorioAbastecimentoModel();
            aux.Data = "Toatal G:";
            aux.Cor = "Lime";
            aux.ValorLitro = tG.ToString("N2");
            relatorioFinal.Add(aux);
            aux = new RelatorioAbastecimentoModel();
            aux.Data = "Media P:";
            aux.Cor = "Lime";
            aux.ValorLitro = (tK/tL).ToString("N3");
           
            relatorioFinal.Add(aux);

            return relatorioFinal;

        }
    }
}

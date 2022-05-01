using Adiministrador.Dao;
using Adiministrador.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Adiministrador.Controller
{
    internal class ReladorioFinancasController
    {

        public List<RelatorioFinancasModel> Relatorio(DateTime inicio, DateTime fim, int idSelecao)
        {
            RelatoriaFinancasDao rd = new RelatoriaFinancasDao();
            try
            {
                List<RelatorioFinancasModel> rm = rd.rela(inicio, fim, idSelecao);

                RelatorioFinancasModel aux = new RelatorioFinancasModel();


                List<RelatorioFinancasModel> relatorioFinal = new List<RelatorioFinancasModel>();///montagem relatorio

                decimal saidasTodal = 0;
                decimal entradasTotal = 0;
                decimal pagamentoDinheiro = 0;
                int auxCor = 0;
                foreach (var s in rm)
                {
                    aux = new RelatorioFinancasModel();
                    if (auxCor == 0)
                    {
                        aux.CorLinha = "Azure";
                        auxCor = 1;
                    }
                    else
                    {
                        aux.CorLinha = "LightYellow";
                        auxCor = 0;
                    }

                    aux.Date = s.Date;

                    switch (s.EntradaSaida)
                    {
                        case "E":
                            aux.Valor = s.Valor;
                            aux.CorTexto = "Green";
                            entradasTotal += (decimal.Parse(s.Valor));
                            //1 - Salario", "2 - Extra", "3 - Doação", "4 - Outro"
                            switch (s.Origem)
                            {
                                case 1:
                                    aux.Conta = "Salario";
                                    break;
                                case 2:
                                    aux.Conta = "Extra";
                                    break;
                                case 3:
                                    aux.Conta = "Doação";
                                    break;
                                case 4:
                                    aux.Conta = "Outro";
                                    break;
                            }

                            break;
                        case "S":
                            if (s.Origem == 5)
                            {
                                aux.Conta = "Saque";
                            }
                            else
                            {
                                aux.Conta = s.Conta;
                            }
                            aux.Valor = "-" + s.Valor;
                            aux.CorTexto = "Red";
                            saidasTodal += (decimal.Parse(s.Valor));
                            break;
                        case "N":
                            aux.Valor = "-" + s.Valor;
                            aux.CorTexto = "Black";
                            aux.Conta = s.Conta;
                            pagamentoDinheiro += (decimal.Parse(s.Valor));
                            break;
                    }

                    ///salva a linha de relatorio
                    relatorioFinal.Add(aux);
                }
                ///Salvar o a soma
                aux = new RelatorioFinancasModel();
                aux.CorLinha = "White";
                relatorioFinal.Add(aux);

                aux = new RelatorioFinancasModel();
                aux.Date = "Total E:";
                aux.CorLinha = "LightSteelBlue";
                aux.CorTexto = "Green";
                aux.Conta = entradasTotal.ToString("N2");
                relatorioFinal.Add(aux);

                aux = new RelatorioFinancasModel();
                aux.Date = "Total S:";
                aux.CorLinha = "LightSteelBlue";
                aux.CorTexto = "Red";
                aux.Conta = "-" + (saidasTodal.ToString("N2"));
                relatorioFinal.Add(aux);

                aux = new RelatorioFinancasModel();
                aux.Date = "Pagamentos em dinheiro:";
                aux.CorLinha = "LightSteelBlue";
                aux.CorTexto = "Black";
                aux.Conta = pagamentoDinheiro.ToString("N2");
                relatorioFinal.Add(aux);

                return relatorioFinal;
               // Gravar();
            }
            catch
            {
                List<RelatorioFinancasModel> da = new List<RelatorioFinancasModel>();
                RelatorioFinancasModel aux = new RelatorioFinancasModel();
                aux.Date = "Falha";
                aux.Conta = "ao caregar";
                aux.Valor = "os Dados";
                aux.CorLinha = "Blue";
                aux.CorTexto = "Red";
                da.Add(aux);
                return da;
            }
        }


     /*   public void Gravar()
        {

            string text = "fdbf\nhhjh\n\nijoiij\n\n";
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "relatorio.pdf");

            File.WriteAllText(fileName, text);
            File.OpenRead(fileName);
            bool doesExist = File.Exists(fileName);//ver se existe


        }*/
    }
}
///https://docs.microsoft.com/pt-br/xamarin/android/deploy-test/release-prep/?tabs=windows
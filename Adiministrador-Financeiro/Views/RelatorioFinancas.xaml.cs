using Adiministrador.Controller;
using Adiministrador.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Adiministrador_Financeiro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelatorioFinancas : ContentPage
    {
        public RelatorioFinancas(DateTime inicio, DateTime fim, int idSelesao)
        {
            InitializeComponent();
             this.consulta(inicio, fim, idSelesao);   

        }
        private void consulta(DateTime inicio, DateTime fim, int idSelesao)
        {
            try
            {

                ReladorioFinancasController rr = new ReladorioFinancasController();


                List<RelatorioFinancasModel> da = new List<RelatorioFinancasModel>();
               
                da = rr.Relatorio(inicio, fim, idSelesao);
                Listagem.ItemsSource = da;
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Relatorio\n"+inicio+"  \n"+fim, "Ok");
            }
        }
    }
}
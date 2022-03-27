using Adiministrador.Controller;
using Adiministrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Adiministrador_Financeiro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Relatorio : ContentPage
    {
        public Relatorio(DateTime inicio,DateTime fim, int veicolo)
        {
            InitializeComponent();
            this.consulta(inicio,fim,veicolo);
        }
        private void consulta(DateTime inicio, DateTime fim, int veicolo)
        {
            try
            {
                RelatorioAbastecimento rr = new RelatorioAbastecimento();


                List<RelatorioAbastecimentoModel> da = new List<RelatorioAbastecimentoModel>();
                da = rr.Relatorio(inicio, fim, veicolo);
                Listagem.ItemsSource = da;
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Relatorio", "Ok");
            }
        }
    }
}

////https://stackoverflow.com/questions/45870535/xamarin-forms-multi-column-table-gui
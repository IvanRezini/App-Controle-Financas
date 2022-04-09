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
    public partial class RelatorioContas : ContentPage
    {
        public RelatorioContas(string usuario)
        {
            InitializeComponent();
            nome.Text = usuario;
            this.CaregarMenu();
        }
        public void CaregarMenu()
        {
            try
            {
                Selecao.Title = "Entradas/Saidas";
               
                    Selecao.Items.Add("1 - Entrada");
                Selecao.Items.Add("2 - Saida");

            }
            catch
            {
                DisplayAlert("Falha", "Falha", "Ok");
               
            }

        }


        async void Button_Clicked(object sender, EventArgs e)
        {
            DateTime inicio = dateInicio.Date;
            DateTime fim = dateFim.Date;
            int IdSelecao = 0;/// 0 é o padrao para trazer contas e receitas
            /*
             * Verifiva se foi selecionado uma opçao
             */
            if (Selecao.SelectedIndex >= 0)
            {
                string aux1 = Selecao.SelectedItem.ToString();
                aux1.Trim();
                string[] aux = aux1.Split('-');
                IdSelecao = int.Parse(aux[0]);
            }

            await Navigation.PushModalAsync(new RelatorioFinancas());
        }

    }

}
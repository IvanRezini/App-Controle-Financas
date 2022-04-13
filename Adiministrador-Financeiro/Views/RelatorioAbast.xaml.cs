using Adiministrador.Dao;
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
    public partial class RelatorioAbast : ContentPage
    {
        public RelatorioAbast(string usuario)
        {
            InitializeComponent();
            nome.Text = usuario;
            this.CaregarMenu();
        }
        public void CaregarMenu()
        {
            try
            {
                DateTime hoje = DateTime.Now;
                DateTime data_passado = hoje.AddDays(-30);
                dateInicio.Date = data_passado;
                VeicoloDao v = new VeicoloDao();
                var aux = new List<String>();
                List<VeicoloModel> vv = v.Get();
                for (int i = 0; i < vv.Count; i++)
                {
                    VeicoloModel pr = vv[i];
                    aux.Add(pr.Id + " - " + pr.Name);
                }
                Selecao.Title = "Selecione um Veicolo:";
                foreach (string x in aux)
                {
                    Selecao.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Veicolos", "Ok");
                Navigation.PopAsync();
            }
        
    }


        async void Button_Clicked(object sender, EventArgs e)
        {
            DateTime inicio = dateInicio.Date;
            DateTime fim = dateFim.Date;
            int IdVeicolo = 0;/// 0 é o padrao para nem um veicolo selecionado
            /*
             * Verifiva se foi selecionado um veicolo
             */
            if (Selecao.SelectedIndex >= 0)
            {
                string aux1 = Selecao.SelectedItem.ToString();
                aux1.Trim();
                string[] aux = aux1.Split('-');
                IdVeicolo = int.Parse(aux[0]);
            }
            
            await Navigation.PushModalAsync(new Relatorio(inicio,fim,IdVeicolo));
        }

    }

}
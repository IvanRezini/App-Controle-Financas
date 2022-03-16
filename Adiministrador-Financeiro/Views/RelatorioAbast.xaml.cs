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
        }
        public void CaregarMenu()
        {
            try
            {
                VeicoloDao v = new VeicoloDao();
                var aux = new List<String>();
                List<VeicoloModel> vv = v.Get();
                for (int i = 0; i < vv.Count; i++)
                {
                    VeicoloModel pr = vv[i];
                    aux.Add(pr.Id + " - " + pr.Name);
                }
                Selecao.Title = "Selecione um Veicolo para alterar.";
                foreach (string x in aux)
                {
                    Selecao.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Dados", "Ok");
            }
        }


        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Relatorio());
        }

    }

}
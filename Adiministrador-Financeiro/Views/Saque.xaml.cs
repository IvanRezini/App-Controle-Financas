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
    public partial class Saque : ContentPage
    {
        public Saque(string usuario)
        {
            InitializeComponent();
            nome.Text = usuario;
            this.caregarMenus();
        }
        private void caregarMenus()
        {
            try
            {
                BancoDao v = new BancoDao();
                var aux = new List<String>();
                List<BancoModel> vv = v.Get();
                for (int i = 0; i < vv.Count; i++)
                {
                    BancoModel pr = vv[i];
                    aux.Add(pr.Id + " - " + pr.Name);
                }
                Banco.Title = "Selecione um banco.";
                foreach (string x in aux)
                {

                    Banco.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Dados", "Ok");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            SaquesModel bancoModel = new SaquesModel();
            Decimal n;
            bool result = Decimal.TryParse(Valor.Text, out n);
            if (result)
            {
                if (Banco.SelectedIndex >= 0)
                {

                    if (await DisplayAlert("Saque", "Valor = " + Valor.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nOrigem do saque: = " + Banco.SelectedItem.ToString(), "ok", "Cancelar"))
                    {
                        bancoModel.Valor = Valor.Text;
                        string[] subs = Banco.SelectedItem.ToString().Trim().Split(' ');
                        bancoModel.IdBanco = Int16.Parse(subs[0]);
                        bancoModel.Data = date.Date;
                        Contexto con = new Contexto();
                        try
                        {
                            con.insert(bancoModel);
                            await DisplayAlert("Cadastro", "Efetuado", "OK");
                        }
                        catch (Exception er)
                        {
                            await DisplayAlert("Cadastro", "Falha\n" + er, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Cancelado", "OK");
                        Valor.Text = "";
                        Banco.SelectedItem = -1;
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "Selecione uma origem", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Valor não coresponde a um numero.", "OK");
            }
        }
    }
}
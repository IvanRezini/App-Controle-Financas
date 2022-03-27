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
    public partial class Receitas : ContentPage
    {
        public Receitas(string usuario)
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

            Origem.Title = "Selecione uma entrada";
            var opcoes = new List<string> { "1 - Salario", "2 - Extra", "3 - Doação", "4 - Outro" };
            foreach (string posto in opcoes)
            {
                Origem.Items.Add(posto);
            }
        }


        private async void salvar_Clicked(object sender, EventArgs e)
        {
            ReceitaModel receitaModel = new ReceitaModel();
            Decimal n;
            bool result = Decimal.TryParse(total.Text, out n);
            if (result)
            {
                if (Origem.SelectedIndex >= 0)
                {
                    if (Banco.SelectedIndex >= 0)
                    {

                        if (await DisplayAlert("Entrada", "Valor = " + total.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nOrigem = " + Origem.SelectedItem.ToString() + "\nBanco = " + Banco.SelectedItem.ToString(), "ok", "Cancelar"))
                        {
                            receitaModel.Valor = total.Text;
                            string[] subs = Origem.SelectedItem.ToString().Trim().Split(' ');
                            receitaModel.Origem = Int16.Parse(subs[0]);
                            subs = Banco.SelectedItem.ToString().Trim().Split(' ');
                            receitaModel.IdBanco = Int16.Parse(subs[0]);
                            receitaModel.Data = date.Date.ToString("yyyy-MM-dd");
                            Contexto con = new Contexto();
                            try
                            {
                                con.insert(receitaModel);
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
                            total.Text = "";
                            Origem.SelectedItem = -1;
                        }

                    }
                    else
                    {
                        await DisplayAlert("Alert", "Selecione um Destino", "OK");
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
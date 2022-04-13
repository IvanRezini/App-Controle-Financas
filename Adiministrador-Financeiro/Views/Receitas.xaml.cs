using Adiministrador.Model;
using System;
using System.Collections.Generic;

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
            Origem.Title = "Selecione uma entrada";
            var opcoes = new List<string> { "1 - Salario", "2 - Extra", "3 - Doação", "4 - Outro" };
            foreach (string posto in opcoes)
            {
                Origem.Items.Add(posto);
            }
        }


        private async void salvar_Clicked(object sender, EventArgs e)
        {
            FinancasModel financasModel = new FinancasModel();
            Decimal n;
            bool result = Decimal.TryParse(total.Text, out n);
            if (result)
            {
                if (Origem.SelectedIndex >= 0)
                {

                    if (await DisplayAlert("Entrada", "Valor = " + total.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nOrigem = " + Origem.SelectedItem.ToString(), "ok", "Cancelar"))
                    {
                        financasModel.Valor = total.Text;
                        string[] subs = Origem.SelectedItem.ToString().Trim().Split(' ');
                        financasModel.Origem = Int16.Parse(subs[0]);
                        financasModel.Data = date.Date.ToString("yyyy-MM-dd");
                        financasModel.EntradaSaida = "E";//E entrada, S saida, N para pagamento em dinheiro no qual ja foi sacado
                        Contexto con = new Contexto();
                        try
                        {
                            con.insert(financasModel);
                            await DisplayAlert("Cadastro", "Efetuado", "OK");
                            this.limparCampos();
                        }
                        catch (Exception er)
                        {
                            await DisplayAlert("Cadastro", "Falha\n" + er, "OK");
                            this.limparCampos();
                        }

                    }
                    else
                    {
                        await DisplayAlert("Alert", "Cancelado", "OK");
                        this.limparCampos();
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
        public void limparCampos()
        {
            total.Text = "";
            Origem.SelectedItem = null;
        }

    }

}
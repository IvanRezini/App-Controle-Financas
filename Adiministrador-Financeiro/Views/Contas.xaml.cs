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
    public partial class Contas : ContentPage
    {
        public Contas(string usuario)
        {
            InitializeComponent();
            nome.Text = usuario;
            this.caregarMenus();
        }
        private void caregarMenus()
        {
            Conta.Title = "Selecione uma conta a pagar";
            var opcoes = new List<string> { "1 - Agua", "2 - Luz", "3 - Condominio", "4 - Internet", "5 - Outras" };
            foreach (string xx in opcoes)
            {
                Conta.Items.Add(xx);
            }
            Banco.Title = "Selecione um Banco";
            opcoes = new List<string> { "1 - BB ", "2 - Caixa", "3 - Outro", "4 - Dinheiro" };
            foreach (string xx in opcoes)
            {
                Banco.Items.Add(xx);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ContasModel contasModel = new ContasModel();
            Decimal n;
            bool result = Decimal.TryParse(Valor.Text, out n);
            if (Conta.SelectedIndex >= 0)
            {
                if (Banco.SelectedIndex >= 0)
                {
                    if (result)
                    {

                        if (await DisplayAlert("Conta", "Conta: " + Conta.SelectedItem.ToString() + "\nValor = " + Valor.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nForma de pagamento: = " + Banco.SelectedItem.ToString(), "ok", "Cancelar"))
                        {
                            contasModel.Valor = Valor.Text;
                            string[] subs = Banco.SelectedItem.ToString().Trim().Split(' ');
                            contasModel.IdBanco = Int16.Parse(subs[0]);
                            contasModel.Data = date.Date.ToString("yyyy-MM-dd");
                            string[] aux = Conta.SelectedItem.ToString().Trim().Split(' ');
                            contasModel.IdConta = Int16.Parse(aux[0]);
                            Contexto con = new Contexto();
                            try
                            {
                                con.insert(contasModel);
                                await DisplayAlert("Salvo", "Efetuado", "OK");
                            }
                            catch (Exception er)
                            {
                                await DisplayAlert("Falha", "Falha\n" + er, "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Cancelado", "OK");
                            Valor.Text = "";
                            Banco.SelectedItem = -1;
                            Conta.SelectedItem = -1;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Valor não coresponde a um numero.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "Selecione como vai ser paga.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Selecione uma conta a ser paga.", "OK");
            }
        }
    }
}
using Adiministrador.Model;
using System;

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
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            FinancasModel financasModel = new FinancasModel();
            Decimal n;
            bool result = Decimal.TryParse(Valor.Text, out n);
            if (result)
            {
                if (await DisplayAlert("Saque", "Valor = " + Valor.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy"), "ok", "Cancelar"))
                {
                    financasModel.Valor = Valor.Text;
                    financasModel.Origem = 5;//origem 5 identifica que é um saque
                    financasModel.EntradaSaida = "S";//E entrada, S saida, N para pagamento em dinheiro no qual ja foi sacado
                    financasModel.Data = date.Date.ToString("yyyy-MM-dd");
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
                await DisplayAlert("Alert", "Valor não coresponde a um numero.", "OK");
            }
        }
        public void limparCampos()
        {
            Valor.Text = "";
        }
    }
}
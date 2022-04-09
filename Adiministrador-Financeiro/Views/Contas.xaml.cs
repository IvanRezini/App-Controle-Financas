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
                try
                {
                    ContaDao p = new ContaDao();
                    var aux = new List<String>();
                    List<ContasModel> pp = p.Get();
                    for (int i = 0; i < pp.Count; i++)
                    {
                        ContasModel pr = pp[i];
                        aux.Add(pr.Id + " - " + pr.Name);
                    }
                    Conta.Title = "Selecione uma conta a pagar";
                    foreach (string x in aux)
                    {
                        Conta.Items.Add(x);
                    }
                }
                catch
                {
                    DisplayAlert("Falha", "Falha ao carregar  o menu", "Ok");
                }


               
            }

            private async void Button_Clicked(object sender, EventArgs e)
        {
            FinancasModel financasModel = new FinancasModel();
            Decimal n;
            bool result = Decimal.TryParse(Valor.Text, out n);
            if (Conta.SelectedIndex >= 0)
            {
                    if (result)
                    {

                        if (await DisplayAlert("Conta", "Conta: " + Conta.SelectedItem.ToString() + "\nValor = " + Valor.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nForma de pagamento: = ", "ok", "Cancelar"))
                        {
                            financasModel.Valor = Valor.Text;
                            financasModel.Data = date.Date.ToString("yyyy-MM-dd");
                            string[] aux = Conta.SelectedItem.ToString().Trim().Split(' ');
                            financasModel.IdConta = Int16.Parse(aux[0]);

                        string forma ="";  //E entrada S saida N para pagamento em dinheiro no qual ja foi sacado
                        if (Dinheiro.IsChecked)
                        {
                            forma = "N";
                        }
                        else
                        {
                            forma = "S";
                        }

                        financasModel.EntradaSaida = forma;
                            Contexto con = new Contexto();
                            try
                            {
                                con.insert(financasModel);
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
                            Conta.SelectedItem = -1;
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
using Adiministrador.Dao;
using Adiministrador.Model;
using System;
using System.Collections.Generic;
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
                for (int i = 1; i < pp.Count; i++)
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
        /*
      * checar a forma de pagamento
      */
        private string verificar()
        {
            string resposta = "";
            if (Dinheiro.IsChecked)
            {
                resposta = "Dinheiro";
            }
            if (CartaoPix.IsChecked)
            {
                resposta = "Cartao / Pix";
            }
            return resposta;
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            FinancasModel financasModel = new FinancasModel();
            Decimal n;
            bool result = Decimal.TryParse(Valor.Text, out n);
            if (Conta.SelectedIndex >= 0 || nomeConta.Text.Length > 2)
            {
                string nomeconta = "";
                if (nomeConta.Text.Length > 0)
                {
                    nomeconta = nomeConta.Text;
                }
                else
                {
                    nomeconta = Conta.SelectedItem.ToString();
                }
                string aux = verificar();
                if (aux != "")
                {

                    if (result)
                    {
                        if (await DisplayAlert("Conta", "Conta: " + nomeconta + "\nValor = " + Valor.Text + "\nData = " + date.Date.ToString("dd-MM-yyyy") + "\nForma de pagamento: = " + aux, "ok", "Cancelar"))
                        {
                            /*
                           * verifica se uma conta foi selecionado ou se
                           * foi inserido um nova conta caso tenha sido inserido ele 
                           * é salvo no banco.
                           */
                            int idConta = 0;
                            Contexto con = new Contexto();
                            if (Conta.SelectedIndex < 0)
                            {
                                ContasModel novo = new ContasModel();
                                novo.Name = nomeConta.Text;
                                try
                                {
                                    con.insert(novo);
                                    ContaDao contaDao = new ContaDao();////ira recuperar o id da conta cadastrado
                                    novo = contaDao.GetName(novo.Name);
                                    idConta = novo.Id;
                                }
                                catch (Exception ex)
                                {
                                    await DisplayAlert("Falha", "Novo posto não cadastrado\n" + ex.ToString(), "ok");
                                }
                            }
                            else
                            {
                                string[] aux1 = Conta.SelectedItem.ToString().Trim().Split('-');
                                idConta = int.Parse(aux1[0]);
                            }
                            financasModel.Valor = Valor.Text;
                            financasModel.Data = date.Date.ToString("yyyy-MM-dd");
                            financasModel.IdConta = idConta;

                            string forma = "";  //E entrada S saida N para pagamento em dinheiro no qual ja foi sacado
                            if (Dinheiro.IsChecked)
                            {
                                forma = "N";
                            }
                            else
                            {
                                forma = "S";
                            }
                            financasModel.EntradaSaida = forma;
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
                        await DisplayAlert("Alert", "Valor não é um numero.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Falha", "Informe uma forma de pagamento:", "Ok");

                }
            }
            else
            {
                await DisplayAlert("Falha", "Informe uma conta ou selecione uma:", "Ok");

            }
        }

        private void nomeConta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Conta.SelectedIndex > 0)
            {
                Conta.SelectedItem = null;
            }
        }

        private void Conta_SelectedIndexChanged(object sender, EventArgs e)
        {
            nomeConta.Text = "";
        }
    }
}
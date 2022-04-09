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
    public partial class Abastecimento : ContentPage
    {

        public Abastecimento(string titulo)
        {
            InitializeComponent();
            nome.Text = titulo;
            this.caregarMenus();

        }
       

        /*
           * Carrega os menus iniciais
           */
        private void caregarMenus()
        {
            try
            {
                PostoDao p = new PostoDao();
                var aux = new List<String>();
                List<PostoModel> pp = p.Get();
                for (int i = 0; i < pp.Count; i++)
                {
                    PostoModel pr = pp[i];
                    aux.Add(pr.Id + " - " + pr.Name);
                }
                Postos.Title = "Selecione um Posto";
                foreach (string x in aux)
                {
                    Postos.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar os postos", "Ok");
            }


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
                Veicolo.Title = "Selecione um Veicolo:";
                foreach (string x in aux)
                {
                    Veicolo.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar Veicolos", "Ok");
                Navigation.PopAsync();
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
        /*
           * Verifica se todos os campos estão preechidos coretament
           */
        private async void salvar_Clicked(object sender, EventArgs e)
        {
            this.preecherCampos();
            Decimal n;
            bool result = false;
            if (Veicolo.SelectedIndex >= 0)
            {
                result = Decimal.TryParse(totalLitros.Text, out n);
                if (result)
                {
                    result = Decimal.TryParse(vlLitro.Text, out n);
                    if (result)
                    {
                        result = Decimal.TryParse(total.Text, out n);
                        string aux = verificar();
                        if (aux!="")
                        {
                            if (result)
                            {
                                if (Postos.SelectedIndex >= 0 || nomePosto.Text.Length > 10)
                                {
                                    result = Decimal.TryParse(km.Text, out n);
                                    if (result)
                                    {
                                        if (await DisplayAlert("Confirmar", "Data: " + date.Date.ToString("d") + "\nTotal: R$ " + total.Text + "\nLitros de gasolina: " +
                                            totalLitros.Text + "\nValor do litro: R$ " + vlLitro.Text +"\nPagamento: "+verificar()+ "\nVeiculo: " +
                                            Veicolo.SelectedItem.ToString() + "\nHodometro: " + ((Decimal.Parse(km.Text)) + (Decimal.Parse(kmTotal.Text))) +
                                            "\nKm percoridos: " + km.Text + "\nMedia: " + ((Decimal.Parse(km.Text)) / (Decimal.Parse(totalLitros.Text))).ToString("F3")
                                            , "Ok", "Cancel"))
                                        {
                                            this.salvar();
                                        }
                                        else
                                        {
                                            await DisplayAlert("Falha", "cancelando", "Ok");
                                            this.limparCampos();
                                        }
                                    }
                                    else
                                    {
                                        await DisplayAlert("Falha", "Informe o km percoridos", "Ok");
                                    }
                                }
                                else
                                {
                                    if (nomePosto.Text.Length > 0 && nomePosto.Text.Length < 10)
                                    {
                                        await DisplayAlert("Falha", "Nome do posto muito curto", "Ok");
                                    }
                                    else
                                    {
                                        await DisplayAlert("Falha", "Informe um posto ou selecione  um", "Ok");
                                    }
                                }
                            }
                            else
                            {
                                await DisplayAlert("Falha", "Informe valor total", "Ok");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Falha", "Selecione uma forma de Pagamento", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Falha", "Informe valor do litro de combustivel", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Falha", "Informe quantia de litros de combustivel", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Falha", "Selecione um veicolo", "Ok");
            }

        }

        private void Veicolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Veicolo.SelectedIndex >= 0)
            {
                string veicolo = Veicolo.SelectedItem.ToString();
                veicolo.Trim();
                string[] aux = veicolo.Split('-');
                int i = int.Parse(aux[0]);
                VeicoloDao vv = new VeicoloDao();
                VeicoloModel veicoloModel = vv.GetID(i);
                kmTotal.Text = veicoloModel.Hodometro;
            }
        }
        /*
           * Verifica e preenche o campo que falta 
           * sendo o usuario apenas preecher dois campos o terceiro 
           * é aotomatizado
           */
        private void preecherCampos()
        {
            Decimal n;
            bool result = false;
            bool result1 = false;
            decimal aux;
            decimal aux1;
            result = Decimal.TryParse(total.Text, out n);
            result1 = Decimal.TryParse(totalLitros.Text, out n);
            if (result && result1)
            {
                aux = Decimal.Parse(total.Text);
                aux1 = Decimal.Parse(totalLitros.Text);
                vlLitro.Text = (aux / aux1).ToString("F3");
            }
            else
            {
                result1 = false;
                result = false;
                result = Decimal.TryParse(total.Text, out n);
                result1 = Decimal.TryParse(vlLitro.Text, out n);

                if (result && result1)
                {
                    aux = Decimal.Parse(total.Text);
                    aux1 = Decimal.Parse(vlLitro.Text);
                    totalLitros.Text = (aux / aux1).ToString("F3");
                }
                else
                {
                    result1 = false;
                    result = false;
                    result = Decimal.TryParse(vlLitro.Text, out n);
                    result1 = Decimal.TryParse(totalLitros.Text, out n);
                    if (result && result1)
                    {
                        aux = Decimal.Parse(vlLitro.Text);
                        aux1 = Decimal.Parse(totalLitros.Text);
                        total.Text = (aux * aux1).ToString("F3");
                    }
                }
            }

        }
        /*
           * Apos o abastecimento ser cadastrado limpa e 0 os campos
           */
        public void limparCampos()
        {
            Veicolo.SelectedItem = null;
            total.Text = "";
            totalLitros.Text = "";
            vlLitro.Text = "";
            nomePosto.Text = "";
            Postos.SelectedItem = null;
            km.Text = "";
            kmTotal.Text = "";
            Dinheiro.IsChecked = false;
            CartaoPix.IsChecked = false;
        }
        /*
            * Salva o abastecimeto
            */
        public async void salvar()
        {
            /*
            * verifica se um posto foi selecionado ou se
            * foi inserido um novo posto caso tenha sido inserido ele 
            * é salvo no banco.
            */
            int idPosto = 0;
            Contexto con = new Contexto();
            if (Postos.SelectedIndex < 0)
            {

                PostoModel novo = new PostoModel();
                novo.Name = nomePosto.Text;

                try
                {
                    con.insert(novo);
                    PostoDao postoDao = new PostoDao();
                    novo = postoDao.GetName(novo.Name);
                    idPosto = novo.Id;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Falha", "Novo posto não cadastrado\n" + ex.ToString(), "ok");
                }

            }
            else
            {
                string[] aux = Postos.SelectedItem.ToString().Trim().Split('-');
                idPosto = int.Parse(aux[0]);

            }

            string forma = "";  //E entrada S saida N para pagamento em dinheiro no qual ja foi sacado
            if (Dinheiro.IsChecked)
            {
                forma = "N";
            }
            else
            {
                forma = "S";
            }
            try
            {
                string aux1;
                AbastecimentoModel ab = new AbastecimentoModel();
                ab.Posto = idPosto;
                aux1 = Veicolo.SelectedItem.ToString();
                aux1.Trim();
                string[] aux = aux1.Split('-');
                ab.Veicolo = int.Parse(aux[0]);
                ab.LitrosTotal = totalLitros.Text;
                ab.ValorLitro = vlLitro.Text;
                aux1 = date.Date.ToString("yyyy-MM-dd");
                ab.Date = aux1;
                ab.kmPercorido = km.Text;
                ab.Hodometro = ((Decimal.Parse(km.Text)) + (Decimal.Parse(kmTotal.Text))).ToString();
                con.insert(ab);

                VeicoloModel veicoloModel = new VeicoloModel();
                veicoloModel.Id = int.Parse(aux[0]);
                veicoloModel.Name = aux[1];
                veicoloModel.Hodometro = ((Decimal.Parse(km.Text)) + (Decimal.Parse(kmTotal.Text))).ToString();
                con.Update(veicoloModel);

                FinancasModel ff = new FinancasModel();
                ff.IdConta = 1;/// Padrao para abastecimento
                ff.Valor= ((Decimal.Parse(totalLitros.Text)) + (Decimal.Parse(vlLitro.Text))).ToString("F3");
                ff.Data = aux1;
                ff.EntradaSaida=forma;
                con.insert(ff);
                 await DisplayAlert("Sucesso", "Abastecimento salvo com exito!!!", "Ok");
                this.limparCampos();
            }
            catch
            {
                await DisplayAlert("Falha", "Ocareu um erro ao salvar", "Ok");
            }

        }
        /*
           * Valida os campos do posto para não ocorer que um posto seja selecionado e
           * um novo posto seja inserido
           */
        private void Postos_SelectedIndexChanged(object sender, EventArgs e)
        {
            nomePosto.Text = "";
        }


        private void nomePosto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Postos.SelectedIndex > 0)
            {
                Postos.SelectedItem = null;
            }
        }
    }
}
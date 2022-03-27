using Adiministrador.Dao;
using Adiministrador.Model;
using Adiministrador_Financeiro.Views;
using System;
using Xamarin.Forms;
using Adiministrador_Financeiro.Controller;

namespace Adiministrador_Financeiro
{
    public partial class MainPage : ContentPage
    {
        string cabeca = "Cadastre seu nome";
        public MainPage()
        {
            InitializeComponent();
            this.titulo();
            this.inicialicacao();
        }
        private void titulo()
        {
            try
            {
                ClienteDao cli = new ClienteDao();
                var cl = cli.GetID(1);
                ClienteModel cliente = cl;
                nome.Text = cl.Name;
                cabeca = cl.Name;
            }
            catch { }
        }
        private void inicialicacao()
        {
            Inicialicacao aux = new Inicialicacao();
            aux.Iniciar();
        }
        private void sair_Clicked(object sender, EventArgs e)
        {

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        async void extratoContas_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new RelatorioContas(cabeca));

        }

        async void extratoAbastecimento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RelatorioAbast(cabeca));
        }

        async void abastecimento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Abastecimento(cabeca));
        }

        async void contasPagas_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new Contas(cabeca));
        }

        async void saques_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Saque(cabeca));
        }

        async void receitas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Receitas(cabeca));
        }

        async void configuracao_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Configuracao(cabeca));
        }
    }
}



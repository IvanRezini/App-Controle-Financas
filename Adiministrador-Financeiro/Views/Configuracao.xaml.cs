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
    public partial class Configuracao : ContentPage
    {
        int model = 0;/// <summary>
                      /// 1- Client
                      /// 2- veicolo
                      /// 3-banco
                      /// 4- posto
                      /// Salva o modelo clicado.
                      /// </summary>

        public Configuracao(string titulo)
        {
            InitializeComponent();
            Usuario.Text = titulo;
            this.ocultarCampos();
        }
        private void ocultarCampos()
        {
            Salvar.IsVisible = false;
            cancelar.IsVisible = false;
            banco.IsEnabled = true;
            posto.IsEnabled = true;
            cliente.IsEnabled = true;
            veicolo.IsEnabled = true;
            Selecao.Items.Clear();
            Selecao.Title = "";
            Selecao.IsEnabled = false;
            Hodometro.Text = "";
            Hodometro.IsVisible = false;
            Nome.Text = "";
            Id.Text = "";
            Nome.IsVisible = false;
            model = 0;
        }
        private void mostrarCampos()
        {
            Salvar.IsVisible = true;
            cancelar.IsVisible = true;
            banco.IsEnabled = false;
            posto.IsEnabled = false;
            cliente.IsEnabled = false;
            veicolo.IsEnabled = false;
            Selecao.IsEnabled = true;
            Nome.IsVisible = true;

        }

        private void banco_Clicked(object sender, EventArgs e)
        {
            this.mostrarCampos();
            Nome.Placeholder = "Cadastre um novo Banco";
            model = 3;
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
                Selecao.Title = "Selecione um banco para alterar.";
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


        private void veicolo_Clicked(object sender, EventArgs e)
        {
            this.mostrarCampos();
            Nome.Placeholder = "Cadastre um novo veicolo";
            Hodometro.IsVisible = true;
            model = 2;
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

        private void cliente_Clicked(object sender, EventArgs e)
        {
            this.mostrarCampos();
            Nome.Placeholder = "Cadastre seu  nome:";
            Selecao.IsEnabled = false;
            model = 1;
            try
            {
                ClienteDao cli = new ClienteDao();
                List<ClienteModel> vv = cli.Get();

                ClienteModel vb = vv[0];
                Id.Text = vb.Id.ToString();
                Nome.Text = vb.Name.ToString();
            }
            catch
            {
                DisplayAlert("Falha", "Nem um úsuario cadastrado\nCadastre seu nome", "Ok");
            }
        }

        private void posto_Clicked(object sender, EventArgs e)
        {
            this.mostrarCampos();
            Nome.Placeholder = "Cadastre o nome de um novo Posto";
            model = 4;
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
                Selecao.Title = "Selecione um Posto para alterar";
                foreach (string x in aux)
                {
                    Selecao.Items.Add(x);
                }
            }
            catch
            {
                DisplayAlert("Falha", "Falha ao caregar os Dados", "Ok");
            }
        }

        /// <summary>
        /// 1- Client
        /// 2- veicolo
        /// 3-banco
        /// 4- posto
        /// Salva o modelo clicado.
        /// </summary>
        private void Selecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Selecao.SelectedIndex >= 0)
            {
                string sele = Selecao.SelectedItem.ToString();
                sele.Trim();
                string[] aux = sele.Split('-');
                Nome.Text = aux[1];
                Id.Text = aux[0];
                if (model == 2)
                {
                    int i = Int16.Parse(aux[0]);
                    VeicoloDao v = new VeicoloDao();
                    var vv = v.GetID(i);
                    VeicoloModel ty = vv;
                    Hodometro.Text = ty.Hodometro;
                }
            }
        }
        /// <summary>
        /// 1- Client
        /// 2- veicolo
        /// 3-banco
        /// 4- posto
        /// Salva o modelo clicado.
        /// </summary>

        private void Salvar_Clicked(object sender, EventArgs e)
        {

            Contexto contexto = new Contexto();
            if (Nome.Text.Trim().Length > 10)
            {
                switch (model)
                {
                    case 1:
                        ClienteModel cli = new ClienteModel();
                        cli.Name = Nome.Text;
                        if (Id.Text.Trim() == "")
                        {
                            contexto.insert(cli);
                        }
                        else
                        {
                            cli.Id = Int16.Parse(Id.Text);
                            contexto.Update(cli);
                        }
                        break;
                    case 2:
                        if (Hodometro.Text.Trim().Length > 5)
                        {
                            VeicoloModel vel = new VeicoloModel();
                            vel.Name = Nome.Text;
                            vel.Hodometro = Hodometro.Text;
                            if (Id.Text.Trim() == "")
                            {
                                contexto.insert(vel);
                            }
                            else
                            {
                                vel.Id = Int16.Parse(Id.Text);
                                contexto.Update(vel);
                            }
                        }
                        else
                        {
                            DisplayAlert("Falha", "O Hodometro deve conter 6 numeros", "Ok");
                            return;
                        }
                        break;
                    case 3:
                        BancoModel ban = new BancoModel();
                        ban.Name = Nome.Text;
                        if (Id.Text.Trim() == "")
                        {
                            contexto.insert(ban);
                        }
                        else
                        {
                            ban.Id = Int16.Parse(Id.Text);
                            contexto.Update(ban);
                        }
                        break;
                    case 4:
                        PostoModel pos = new PostoModel();
                        pos.Name = Nome.Text;
                        if (Id.Text.Trim() == "")
                        {
                            contexto.insert(pos);
                        }
                        else
                        {
                            pos.Id = Int16.Parse(Id.Text);
                            contexto.Update(pos);
                        }
                        break;
                }
                DisplayAlert("Sucesso", "Operaçao realizada", "Ok");
                this.ocultarCampos();
            }
            else
            {
                DisplayAlert("Falha", "O campo nome deve conter no minimo 10 caracteres", "Ok");
            }

        }

        private void cancelar_Clicked(object sender, EventArgs e)
        {
            this.ocultarCampos();
        }
    }
}
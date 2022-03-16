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
    public partial class RelatorioContas : ContentView
    {
        public RelatorioContas(string usuario)
        {
            InitializeComponent();
            nome.Text = usuario;
        }
    }
}
using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Pessoas
{
    public partial class CadastroPessoaUserControl : UserControl
    {
        public CadastroPessoaUserControl()
        {
            InitializeComponent();
            DataContext = new PessoaViewModel();
        }
    }
}
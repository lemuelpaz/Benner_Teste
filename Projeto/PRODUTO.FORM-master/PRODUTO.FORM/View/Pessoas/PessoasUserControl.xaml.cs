using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Pessoas
{
    public partial class PessoasUserControl : UserControl
    {
        public PessoasUserControl()
        {
            InitializeComponent();
            DataContext = new PessoaViewModel();
        }
    }
}
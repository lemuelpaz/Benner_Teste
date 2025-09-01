using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Pessoas
{
    public partial class EditarPessoasUserControl : UserControl
    {
        public EditarPessoasUserControl()
        {
            InitializeComponent();
            DataContext = new PessoaViewModel();
        }

       
    }
}
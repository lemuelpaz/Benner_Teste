using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Produtos
{
    public partial class ProdutosUserControl : UserControl
    {
        public ProdutosUserControl()
        {
            InitializeComponent();
            DataContext = new ProdutoViewModel(); 
        }
    }
}

using PRODUTO.FORM.Models;
using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Produtos
{
    public partial class EditarProdutoUserControl1 : UserControl
    {
        public ProdutoViewModel ViewModel { get; }

        public EditarProdutoUserControl1(Produto produto)
        {
            InitializeComponent();
            ViewModel = new ProdutoViewModel();
            ViewModel.ProdutoSelecionado = produto;
            DataContext = ViewModel;
        }
    }
}
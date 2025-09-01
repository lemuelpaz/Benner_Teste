using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Pedidos
{
    public partial class PedidosUserControl : UserControl
    {
        public PedidosUserControl()
        {
            InitializeComponent();
            DataContext = new PedidoViewModel();
        }
    }
}
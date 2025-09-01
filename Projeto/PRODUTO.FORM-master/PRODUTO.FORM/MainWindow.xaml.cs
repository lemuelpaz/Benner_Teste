using PRODUTO.FORM.View;
using PRODUTO.FORM.View.Dashboard;
using PRODUTO.FORM.View.Pedidos;
using PRODUTO.FORM.View.Pessoas;
using PRODUTO.FORM.View.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRODUTO.FORM
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new DashboardUserControl();
        }
       
        private void Dashboard_Click(object sender, RoutedEventArgs args)
        {
            MainContent.Content = new DashboardUserControl();
        }
        private void Pessoas_Click(object sender, RoutedEventArgs args)
        {
            MainContent.Content = new PessoasUserControl();
        }
        private void Produtos_Click(object sender, RoutedEventArgs args)
        {
            MainContent.Content = new ProdutosUserControl();
        }
        public void NavigateToCadastroProduto()
        {
            MainContent.Content = new CadastroProdutoUserControl();
        }
        public void NavigateToCadastroPessoa()
        {
            MainContent.Content = new CadastroPessoaUserControl();
        }
        private void Pedidos_Click(object sender, RoutedEventArgs args)
        {
            MainContent.Content = new PedidosUserControl();
        }
    }
}

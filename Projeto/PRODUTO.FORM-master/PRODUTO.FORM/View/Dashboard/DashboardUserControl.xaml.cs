using PRODUTO.FORM.ViewModels;
using System.Windows.Controls;

namespace PRODUTO.FORM.View.Dashboard
{
    public partial class DashboardUserControl : UserControl
    {
        public DashboardUserControl()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(); 
        }
    }
}

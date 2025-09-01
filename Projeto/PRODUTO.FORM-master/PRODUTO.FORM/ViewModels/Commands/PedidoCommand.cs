using PRODUTO.FORM.ViewModels;
using System;
using System.Windows.Input;

namespace PRODUTO.FORM.ViewModels.Commands
{
    public class PedidoCommand : ICommand
    {
        private readonly PedidoViewModel _viewModel;

        public PedidoCommand(PedidoViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null) return;

            string action = parameter.ToString();

            switch (action)
            {
                case "AdicionarProduto":
                    _viewModel.AdicionarProduto();
                    break;

                case "FinalizarPedido":
                    _viewModel.FinalizarPedido();
                    break;

                case "LimparItens":
                    _viewModel.ItensPedido.Clear();                    
                    break;

                case "Voltar":
                    if (System.Windows.Application.Current.MainWindow is PRODUTO.FORM.MainWindow mainWindow)
                        mainWindow.MainContent.Content = new PRODUTO.FORM.View.Pedidos.PedidosUserControl(); // ou outra tela
                    break;
            }
        }

    }
}

using PRODUTO.FORM.ViewModels;
using System;
using System.Windows.Input;

namespace PRODUTO.FORM.ViewModels.Commands
{
    public class DashboardCommand : ICommand
    {
        private readonly DashboardViewModel _viewModel;

        public DashboardCommand(DashboardViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            // Sempre pode executar; você pode adicionar lógica se precisar
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null) return;

            string action = parameter.ToString();

            switch (action)
            {
                case "Atualizar":
                    _viewModel.CarregarDados(); // Atualiza todos os valores do dashboard
                    break;

                    
            }
        }
    }
}

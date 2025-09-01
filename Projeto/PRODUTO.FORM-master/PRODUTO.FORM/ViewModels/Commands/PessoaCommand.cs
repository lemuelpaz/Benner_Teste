using PRODUTO.FORM.Models;
using System;
using System.Windows.Input;

namespace PRODUTO.FORM.ViewModels.Commands
{
    public class PessoaCommand : ICommand
    {
        private readonly PessoaViewModel _viewModel;

        public PessoaCommand(PessoaViewModel viewModel)
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
            if (parameter is string action)
            {
                switch (action)
                {
                    case "Carregar":
                        _viewModel.CarregarPessoas();
                        break;
                    case "Pesquisar":
                        _viewModel.AplicarFiltros();
                        break;
                    case "Limpar":
                        _viewModel.LimparFiltros();
                        break;
                    case "Salvar":
                        _viewModel.SalvarAlteracoes();
                        break;
                    case "Incluir":                 
                            _viewModel.AbrirCadastro();
                      
                        break;
                    case "Excluir":
                        _viewModel.ExcluirPessoa(_viewModel.PessoaSelecionada);
                        break;
                    case "Voltar":
                        _viewModel.Voltar();
                        break;
                    case "IncluirPessoa":
                        _viewModel.IncluirPessoa(); 
                        break;
                    case "SalvarEdicao":
                        _viewModel.SalvarEdicao();
                        break;
                    case "Editar":
                        _viewModel.AbrirEdicao(_viewModel.PessoaSelecionada);
                        break;
                }
            }            
        }
    }
}

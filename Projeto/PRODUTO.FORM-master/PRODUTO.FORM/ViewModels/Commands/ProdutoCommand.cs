using PRODUTO.FORM.Models;
using PRODUTO.FORM.Service;
using PRODUTO.FORM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PRODUTO.FORM.Commands
{
    public class ProdutoCommand : ICommand
    {
        private readonly ProdutoViewModel _viewModel;

        public ProdutoCommand(ProdutoViewModel viewModel)
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
                        _viewModel.CarregarProdutos();
                        break;
                    case "Pesquisar":
                        _viewModel.AplicarFiltros();
                        break;
                    case "Limpar":
                        _viewModel.LimparFiltros();
                        break;
                    case "Incluir":
                        _viewModel.NavegarParaCadastro();
                        break;
                    case "Editar":
                        _viewModel.EditarProduto();
                        break;
                    case "Excluir":
                        _viewModel.ExcluirProduto();
                        break;
                    case "SalvarEdicao":
                        _viewModel.SalvarEdicaoProduto(); 
                        break;
                    case "Voltar":
                        _viewModel.VoltarParaLista(); 
                        break;
                    case "SalvarCadastro":
                        _viewModel.SalvarCadastro();
                        break;
                }
            }
        }
    }
}

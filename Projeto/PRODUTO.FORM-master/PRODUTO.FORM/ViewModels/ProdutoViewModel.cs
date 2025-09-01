using PRODUTO.FORM.Commands;
using PRODUTO.FORM.Models;
using PRODUTO.FORM.Service;
using PRODUTO.FORM.View.Produtos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PRODUTO.FORM.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Produto> Produtos { get; set; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                _produtoSelecionado = value;
                OnPropertyChanged(nameof(ProdutoSelecionado));
            }
        }

        public string FiltroNome { get; set; }
        public string FiltroCodigo { get; set; }

        public ProdutoCommand ProdutoCommand { get; }

        public ProdutoViewModel()
        {
            Produtos = new ObservableCollection<Produto>(ProdutoService.CarregarProdutos());
            ProdutoCommand = new ProdutoCommand(this);
            ProdutoSelecionado = new Produto();
        }

        public void CarregarProdutos()
        {
            Produtos = new ObservableCollection<Produto>(ProdutoService.CarregarProdutos());
            OnPropertyChanged(nameof(Produtos));
        }

        public void AplicarFiltros()
        {
            var produtos = ProdutoService.CarregarProdutos().AsQueryable();

            if (!string.IsNullOrWhiteSpace(FiltroNome))
                produtos = produtos.Where(p => p.Nome.ToLower().Contains(FiltroNome.ToLower()));

            if (!string.IsNullOrWhiteSpace(FiltroCodigo))
                produtos = produtos.Where(p => p.Codigo.ToLower().Contains(FiltroCodigo.ToLower()));

            Produtos = new ObservableCollection<Produto>(produtos);
            OnPropertyChanged(nameof(Produtos));
        }

        public void LimparFiltros()
        {
            FiltroNome = string.Empty;
            FiltroCodigo = string.Empty;
            CarregarProdutos();
            OnPropertyChanged(nameof(FiltroNome));
            OnPropertyChanged(nameof(FiltroCodigo));
        }

        public void NavegarParaCadastro()
        {
            ProdutoSelecionado = new Produto();
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
                mainWindow.MainContent.Content = new CadastroProdutoUserControl();
        }

        public void EditarProduto()
        {
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto para editar!");
                return;
            }

            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
                mainWindow.MainContent.Content = new EditarProdutoUserControl1(ProdutoSelecionado);
        }

        public void ExcluirProduto()
        {
            if (ProdutoSelecionado == null) return;

            var resultado = MessageBox.Show($"Deseja realmente excluir o produto '{ProdutoSelecionado.Nome}'?",
                                            "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                bool sucesso = ProdutoService.ExcluirProduto(ProdutoSelecionado.Id);
                if (sucesso)
                {
                    MessageBox.Show("Produto excluído com sucesso!");
                    CarregarProdutos();
                }
            }
        }

        public void VoltarParaLista()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
                mainWindow.MainContent.Content = new ProdutosUserControl();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SalvarEdicaoProduto()
        {
            if (ProdutoSelecionado == null) return;

            // --- Validação obrigatória
            if (string.IsNullOrWhiteSpace(ProdutoSelecionado.Nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ProdutoSelecionado.Codigo))
            {
                MessageBox.Show("O campo Código é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ProdutoSelecionado.Preco <= 0)
            {
                MessageBox.Show("O campo Valor é obrigatório e deve ser maior que zero.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool sucesso = ProdutoService.EditarProduto(ProdutoSelecionado);

            if (sucesso)
            {
                MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                VoltarParaLista();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar o produto.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SalvarCadastro()
        {
            if (ProdutoSelecionado == null) return;

            if (string.IsNullOrWhiteSpace(ProdutoSelecionado.Nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ProdutoSelecionado.Codigo))
            {
                MessageBox.Show("O campo Código é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ProdutoSelecionado.Preco <= 0)
            {
                MessageBox.Show("O campo Valor é obrigatório e deve ser maior que zero.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // --- Persistência
            var listaProdutos = ProdutoService.CarregarProdutos();
            ProdutoSelecionado.Id = listaProdutos.Any() ? listaProdutos.Max(p => p.Id) + 1 : 1;
            listaProdutos.Add(ProdutoSelecionado);
            ProdutoService.SalvarProdutos(listaProdutos);

            MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            VoltarParaLista();
        }

    }
}

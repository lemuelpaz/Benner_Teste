using PRODUTO.FORM.Models;
using PRODUTO.FORM.Service;
using PRODUTO.FORM.Services;
using PRODUTO.FORM.View.Pessoas;
using PRODUTO.FORM.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PRODUTO.FORM.ViewModels
{
    public class PessoaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public Pessoa PessoaSelecionada { get; set; }

        public Pessoa NovaPessoa { get; set; } = new Pessoa();

        public string FiltroNome { get; set; }
        public string FiltroCPF { get; set; }

        public PessoaCommand PessoaCommand { get; }

        public PessoaViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(PessoaService.CarregarPessoas());
            CarregarPedidos();
            PessoaCommand = new PessoaCommand(this);
        }

        private void CarregarPedidos()
        {
            foreach (var pessoa in Pessoas)
            {
                pessoa.Pedidos = PedidoService.ObterPedidosPorPessoa(pessoa.Id);
            }
        }

        public void CarregarPessoas()
        {
            Pessoas = new ObservableCollection<Pessoa>(PessoaService.CarregarPessoas());
            CarregarPedidos();
            OnPropertyChanged(nameof(Pessoas));
        }

        public void AplicarFiltros()
        {
            var filtro = Pessoas.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(FiltroNome))
                filtro = filtro.Where(p => p.Nome.ToLower().Contains(FiltroNome.ToLower()));

            if (!string.IsNullOrWhiteSpace(FiltroCPF))
                filtro = filtro.Where(p => p.CPF.ToLower().Contains(FiltroCPF.ToLower()));

            Pessoas = new ObservableCollection<Pessoa>(filtro);
            OnPropertyChanged(nameof(Pessoas));
        }

        public void LimparFiltros()
        {
            FiltroNome = string.Empty;
            FiltroCPF = string.Empty;
            CarregarPessoas();
            OnPropertyChanged(nameof(FiltroNome));
            OnPropertyChanged(nameof(FiltroCPF));
        }

        public void SelecionarPessoa(Pessoa pessoa)
        {
            PessoaSelecionada = pessoa;
            OnPropertyChanged(nameof(PessoaSelecionada));
        }

        public void SalvarAlteracoes()
        {
            try
            {
                if (Pessoas != null)
                {
                    PessoaService.SalvarPessoas(Pessoas.ToList());
                    MessageBox.Show("Alterações salvas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nenhuma pessoa encontrada para salvar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void IncluirPessoa()
        {
            if (NovaPessoa == null) return;

            if (string.IsNullOrWhiteSpace(NovaPessoa.Nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NovaPessoa.CPF))
            {
                MessageBox.Show("O campo CPF é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var lista = Pessoas.ToList();
            lista.Add(NovaPessoa);
            Pessoas = new ObservableCollection<Pessoa>(lista);
            PessoaService.SalvarPessoas(lista);

            MessageBox.Show("Pessoa cadastrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            NovaPessoa = new Pessoa();
            OnPropertyChanged(nameof(Pessoas));
            OnPropertyChanged(nameof(NovaPessoa));

            Voltar();
        }

        public void AbrirCadastro()
        {
            NovaPessoa = new Pessoa();
            OnPropertyChanged(nameof(NovaPessoa));

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainContent.Content = new CadastroPessoaUserControl
                {
                    DataContext = this
                };
            }
        }

        public void Voltar()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainContent.Content = new PessoasUserControl();
            }
        }

        public void ExcluirPessoa(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                MessageBox.Show("Selecione uma pessoa para excluir!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show(
                $"Deseja realmente excluir a pessoa {pessoa.Nome}?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (resultado == MessageBoxResult.Yes)
            {
                if (PessoaService.ExcluirPessoa(pessoa.Id))
                {
                    MessageBox.Show("Pessoa excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    Pessoas.Remove(pessoa);
                }
            }
        }
        public Pessoa PessoaEditando { get; set; } = new Pessoa();

        public void AbrirEdicao(Pessoa pessoaSelecionada)
        {
            if (pessoaSelecionada == null) return;

            PessoaEditando = new Pessoa
            {
                Id = pessoaSelecionada.Id,
                Nome = pessoaSelecionada.Nome,
                CPF = pessoaSelecionada.CPF,
                Endereco = pessoaSelecionada.Endereco
            };
            OnPropertyChanged(nameof(PessoaEditando));

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                var editarUC = new EditarPessoasUserControl();
                editarUC.DataContext = this; 
                mainWindow.MainContent.Content = editarUC;
            }
        }


        // Método para salvar edição
        public void SalvarEdicao()
        {
            if (PessoaEditando == null) return;

            if (string.IsNullOrWhiteSpace(PessoaEditando.Nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PessoaEditando.CPF))
            {
                MessageBox.Show("O campo CPF é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var pessoa = Pessoas.FirstOrDefault(p => p.Id == PessoaEditando.Id);
            if (pessoa != null)
            {
                pessoa.Nome = PessoaEditando.Nome;
                pessoa.CPF = PessoaEditando.CPF;
                pessoa.Endereco = PessoaEditando.Endereco;

                PessoaService.EditarPessoa(pessoa);
                OnPropertyChanged(nameof(Pessoas));
                MessageBox.Show("Pessoa atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                Voltar();
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

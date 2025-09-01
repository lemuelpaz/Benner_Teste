using PRODUTO.FORM.Models;
using PRODUTO.FORM.Service;
using PRODUTO.FORM.Services;
using PRODUTO.FORM.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PRODUTO.FORM.ViewModels
{
    public class PedidoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string ArquivoPedidos = "pedidos.json";

        public ObservableCollection<Pessoa> Clientes { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<PedidoItemView> ItensPedido { get; set; } = new ObservableCollection<PedidoItemView>();

        public Pessoa ClienteSelecionado { get; set; }
        public Produto ProdutoSelecionado { get; set; }
        public string QuantidadeProduto { get; set; }
        public string ValorTotal { get; set; }
        public string FormaPagamento { get; set; }

        public PedidoCommand PedidoCommand { get; }


        public PedidoViewModel()
        {
            PedidoCommand = new PedidoCommand(this);
            CarregarClientes();
            CarregarProdutos();
            AtualizarValorTotal();
        }

        private void CarregarClientes()
        {
            var clientes = PessoaService.CarregarPessoas();
            Clientes = new ObservableCollection<Pessoa>(clientes);
            OnPropertyChanged(nameof(Clientes));
        }

        private void CarregarProdutos()
        {
            var produtos = ProdutoService.CarregarProdutos();
            Produtos = new ObservableCollection<Produto>(produtos);
            OnPropertyChanged(nameof(Produtos));
        }

        public void AdicionarProduto()
        {
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantidadeProduto, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Informe uma quantidade válida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var itemExistente = ItensPedido.FirstOrDefault(i => i.Produto.Id == ProdutoSelecionado.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
                itemExistente.Subtotal = itemExistente.Quantidade * itemExistente.Preco;
            }
            else
            {
                ItensPedido.Add(new PedidoItemView
                {
                    Produto = ProdutoSelecionado,
                    Quantidade = quantidade,
                    Codigo = ProdutoSelecionado.Codigo,
                    Nome = ProdutoSelecionado.Nome,
                    Preco = ProdutoSelecionado.Preco,
                    Subtotal = ProdutoSelecionado.Preco * quantidade
                });
            }

            AtualizarValorTotal();
            OnPropertyChanged(nameof(ItensPedido));
        }

        private void AtualizarValorTotal()
        {
            foreach (var item in ItensPedido)
            {
                item.Subtotal = item.Produto.Preco * item.Quantidade;
            }

            ValorTotal = "R$ " + ItensPedido.Sum(i => i.Subtotal).ToString("N2");
            OnPropertyChanged(nameof(ValorTotal));
        }

        public void FinalizarPedido()
        {
            if (ClienteSelecionado == null)
            {
                MessageBox.Show("Selecione um cliente.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ItensPedido.Any())
            {
                MessageBox.Show("Adicione pelo menos um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var pedido = new Pedido
            {
                Id = GerarIdPedido(),
                Pessoa = ClienteSelecionado,
                Produtos = ItensPedido.Select(i => new PedidoItem
                {
                    Produto = i.Produto,
                    Quantidade = i.Quantidade
                }).ToList(),
                FormaPagamento = FormaPagamento ?? "Não informado",
                DataVenda = DateTime.Now
            };

            SalvarPedido(pedido);

            MessageBox.Show("Pedido finalizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            ItensPedido.Clear();
            AtualizarValorTotal();
        }

        private int GerarIdPedido()
        {
            var pedidosExistentes = CarregarPedidos();
            return pedidosExistentes.Any() ? pedidosExistentes.Max(p => p.Id) + 1 : 1;
        }

        private List<Pedido> CarregarPedidos()
        {
            if (!File.Exists(ArquivoPedidos))
                return new List<Pedido>();

            var json = File.ReadAllText(ArquivoPedidos);
            return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        }

        private void SalvarPedido(Pedido pedido)
        {
            if (pedido.Pessoa == null)
            {
                MessageBox.Show("O campo Pessoa é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (pedido.Produtos == null || !pedido.Produtos.Any())
            {
                MessageBox.Show("O pedido deve conter pelo menos um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(pedido.FormaPagamento))
            {
                MessageBox.Show("O campo Forma de Pagamento é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var pedidosExistentes = CarregarPedidos();
            pedidosExistentes.Add(pedido);
            var json = JsonSerializer.Serialize(pedidosExistentes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ArquivoPedidos, json);

            MessageBox.Show("Pedido salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class PedidoItemView
        {
            public Produto Produto { get; set; }
            public int Quantidade { get; set; }
            public string Codigo { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}

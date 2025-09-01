using PRODUTO.FORM.Service;
using PRODUTO.FORM.Services;
using System;
using System.ComponentModel;
using System.Linq;

namespace PRODUTO.FORM.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private string _qtdPessoas;
        public string QtdPessoas
        {
            get => _qtdPessoas;
            set { _qtdPessoas = value; OnPropertyChanged(nameof(QtdPessoas)); }
        }

        
        private string _qtdProdutos;
        public string QtdProdutos
        {
            get => _qtdProdutos;
            set { _qtdProdutos = value; OnPropertyChanged(nameof(QtdProdutos)); }
        }

        
        private string _qtdPedidos;
        public string QtdPedidos
        {
            get => _qtdPedidos;
            set { _qtdPedidos = value; OnPropertyChanged(nameof(QtdPedidos)); }
        }

        private string _valorTotalVendas;
        public string ValorTotalVendas
        {
            get => _valorTotalVendas;
            set { _valorTotalVendas = value; OnPropertyChanged(nameof(ValorTotalVendas)); }
        }

        private string _pedidosPendentesQtd;
        public string PedidosPendentesQtd
        {
            get => _pedidosPendentesQtd;
            set { _pedidosPendentesQtd = value; OnPropertyChanged(nameof(PedidosPendentesQtd)); }
        }

        private string _pedidosPendentesValor;
        public string PedidosPendentesValor
        {
            get => _pedidosPendentesValor;
            set { _pedidosPendentesValor = value; OnPropertyChanged(nameof(PedidosPendentesValor)); }
        }

        private string _pedidosPagosQtd;
        public string PedidosPagosQtd
        {
            get => _pedidosPagosQtd;
            set { _pedidosPagosQtd = value; OnPropertyChanged(nameof(PedidosPagosQtd)); }
        }

        private string _pedidosPagosValor;
        public string PedidosPagosValor
        {
            get => _pedidosPagosValor;
            set { _pedidosPagosValor = value; OnPropertyChanged(nameof(PedidosPagosValor)); }
        }

        public DashboardViewModel()
        {
            CarregarDados();
        }

        public void CarregarDados()
        {
            var pessoas = PessoaService.CarregarPessoas();
            QtdPessoas = pessoas.Count.ToString();

            var produtos = ProdutoService.CarregarProdutos();
            QtdProdutos = produtos.Count.ToString();

            var pedidos = PedidoService.ObterTodosPedidos();
            QtdPedidos = pedidos.Count.ToString();

            var valorTotal = pedidos.Sum(p => p.PrecoTotal);
            ValorTotalVendas = "R$ " + valorTotal.ToString("N2");

            var pedidosPendentes = pedidos.Where(p => p.Status == "Pendente").ToList();
            PedidosPendentesQtd = pedidosPendentes.Count.ToString();
            PedidosPendentesValor = "R$ " + pedidosPendentes.Sum(p => p.PrecoTotal).ToString("N2");

            var pedidosPagos = pedidos.Where(p => p.Status == "Pago").ToList();
            PedidosPagosQtd = pedidosPagos.Count.ToString();
            PedidosPagosValor = "R$ " + pedidosPagos.Sum(p => p.PrecoTotal).ToString("N2");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace PRODUTO.FORM.Models
{
	public class Pedido
	{
		public int Id { get; set; }
		public Pessoa Pessoa { get; set; }
		public List<PedidoItem> Produtos { get; set; } = new List<PedidoItem>();
		public decimal PrecoTotal => Produtos.Sum(p => p.Quantidade * p.Produto.Preco);
        public decimal ValorTotal => PrecoTotal;
        public DateTime DataVenda { get; set; }
		public string FormaPagamento { get; set; }
		public string Status { get; set; } = "Pendente";
	}

}

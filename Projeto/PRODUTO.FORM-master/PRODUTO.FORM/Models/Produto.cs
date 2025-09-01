using PRODUTO.FORM.Models;

namespace PRODUTO.FORM.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public bool IsSelected { get; set; } = false;

    }
}

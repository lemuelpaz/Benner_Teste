using PRODUTO.FORM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PRODUTO.FORM.Service
{
    public static class ProdutoService
    {
        private static readonly string CaminhoJson = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "DataProduto.json"
        );

        public static List<Produto> CarregarProdutos()
        {
            if (!File.Exists(CaminhoJson))
            {
                MessageBox.Show($"Arquivo não encontrado: {CaminhoJson}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Produto>();
            }

            try
            {
                string json = File.ReadAllText(CaminhoJson);
                var produtos = JsonSerializer.Deserialize<List<Produto>>(json);
                return produtos ?? new List<Produto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler produtos.json: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Produto>();
            }
        }

        public static void SalvarProdutos(List<Produto> produtos)
        {
            try
            {
                string json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CaminhoJson, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar produtos.json: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        public static bool ExcluirProduto(int id)
        {
            try
            {
                var produtos = CarregarProdutos();
                var produtoParaExcluir = produtos.FirstOrDefault(p => p.Id == id);

                if (produtoParaExcluir == null)
                {
                    MessageBox.Show("Produto não encontrado!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                produtos.Remove(produtoParaExcluir);
                SalvarProdutos(produtos);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

      
        public static bool EditarProduto(Produto produtoEditado)
        {
            try
            {
                if (produtoEditado == null)
                {
                    MessageBox.Show("Produto inválido!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var produtos = CarregarProdutos();
                var produtoExistente = produtos.FirstOrDefault(p => p.Id == produtoEditado.Id);

                if (produtoExistente == null)
                {
                    MessageBox.Show("Produto não encontrado!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

               
                produtoExistente.Nome = produtoEditado.Nome;
                produtoExistente.Codigo = produtoEditado.Codigo;
                produtoExistente.Preco = produtoEditado.Preco;
                produtoExistente.Descricao = produtoEditado.Descricao;

                
                SalvarProdutos(produtos);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}

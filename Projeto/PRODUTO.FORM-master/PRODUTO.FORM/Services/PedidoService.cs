using PRODUTO.FORM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PRODUTO.FORM.Services
{
    internal static class PedidoService
    {
        private static readonly string CaminhoJson = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "DataPedido.json"
        );

        public static List<Pedido> ObterTodosPedidos()
        {
            return CarregarPedidos();
        }

        public static List<Pedido> ObterPedidosPorPessoa(int pessoaId)
        {
            return CarregarPedidos().Where(p => p.Pessoa.Id == pessoaId).ToList();
        }
        public static void SalvarPedido(Pedido pedido)
        {
            try
            {
                var pedidos = CarregarPedidos();

                pedido.Id = pedidos.Any() ? pedidos.Max(p => p.Id) + 1 : 1;

                pedidos.Add(pedido);

                string json = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });

                Directory.CreateDirectory(Path.GetDirectoryName(CaminhoJson));

                File.WriteAllText(CaminhoJson, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar pedido: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static List<Pedido> CarregarPedidos()
        {
            if (!File.Exists(CaminhoJson))
                return new List<Pedido>();

            try
            {
                string json = File.ReadAllText(CaminhoJson);
                return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar pedidos: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pedido>();
            }
        }

        public static List<string> ObterDatasPedidos()
        {
            return ObterTodosPedidos()
                    .Select(p => p.DataVenda.ToString("dd/MM/yyyy"))
                    .Distinct()
                    .ToList();
        }
    }
}

using Newtonsoft.Json;
using PRODUTO.FORM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PRODUTO.FORM.Service
{
    public static class PessoaService
    {
       
        private static readonly string CaminhoJson = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "DataPessoa.json"
        );

        public static List<Pessoa> CarregarPessoas()
        {
            if (!File.Exists(CaminhoJson))
            {
                MessageBox.Show($"Arquivo não encontrado: {CaminhoJson}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pessoa>();
            }

            try
            {
                string json = File.ReadAllText(CaminhoJson);
                var pessoas = System.Text.Json.JsonSerializer.Deserialize<List<Pessoa>>(json);
                return pessoas ?? new List<Pessoa>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler pessoas.json: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pessoa>();
            }
        }
        public static void SalvarPessoas(List<Pessoa> pessoas)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = System.Text.Json.JsonSerializer.Serialize(pessoas, options);
                File.WriteAllText(CaminhoJson, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar pessoas.json: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static bool ExcluirPessoa(int id)
        {
            try
            {
                var pessoas = CarregarPessoas();
                var pessoaParaExcluir = pessoas.FirstOrDefault(p => p.Id == id);

                if (pessoaParaExcluir == null)
                {
                    MessageBox.Show("Pessoa não encontrada!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                pessoas.Remove(pessoaParaExcluir);
                SalvarPessoas(pessoas);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir pessoa: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool IncluirPessoa(Pessoa novaPessoa)
        {
            try
            {
                if (novaPessoa == null)
                {
                    MessageBox.Show("Pessoa inválida!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var pessoas = CarregarPessoas();

                int novoId = pessoas.Any() ? pessoas.Max(p => p.Id) + 1 : 1;
                novaPessoa.Id = novoId;

                pessoas.Add(novaPessoa);
                SalvarPessoas(pessoas);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao incluir pessoa: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool EditarPessoa(Pessoa pessoaAtualizada)
        {
            try
            {
                if (pessoaAtualizada == null)
                {
                    MessageBox.Show("Pessoa inválida!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var pessoas = CarregarPessoas();
                var pessoaExistente = pessoas.FirstOrDefault(p => p.Id == pessoaAtualizada.Id);

                if (pessoaExistente == null)
                {
                    MessageBox.Show("Pessoa não encontrada!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                pessoaExistente.Nome = pessoaAtualizada.Nome;
                pessoaExistente.CPF = pessoaAtualizada.CPF;
                pessoaExistente.Endereco = pessoaAtualizada.Endereco;

                SalvarPessoas(pessoas);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar pessoa: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}

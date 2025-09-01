# Projeto WPF - Cadastro de Pessoas e Pedidos

## Descrição
Este projeto é uma aplicação **WPF** desenvolvida em **C#**, destinada ao cadastro de pessoas e pedidos.  
O sistema utiliza **JSON** para armazenar dados localmente, permitindo realizar operações de CRUD (Create, Read, Update, Delete) em pessoas e pedidos.

O layout segue um padrão moderno com **UserControls**, DataGrids e botões estilizados.

---

## Funcionalidades

### Pessoas
- Cadastro de pessoas (Nome, CPF, Endereço)
- Listagem de pessoas
- Edição e exclusão de registros
- Filtragem por Nome e CPF

### Pedidos
- Cadastro de pedidos vinculados a pessoas
- Listagem de pedidos
- Filtragem por status (entregues, pagos, pendentes)
- Exibição de valores totais

### Interface
- Botões estilizados com cores consistentes
- DataGrid para exibição e edição de dados
- Navegação entre telas usando UserControls

---

## Tecnologias

- **C# 12 / .NET 8**
- **WPF**
- **MVVM**
- **JSON** para persistência de dados
- Bibliotecas recomendadas:
  - `Newtonsoft.Json` ou `System.Text.Json` para serialização/deserialização JSON


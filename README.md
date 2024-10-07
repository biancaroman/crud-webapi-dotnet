# API de Livros - .NET Web API com MongoDB

Esta é uma aplicação ASP.NET Core Web API para gerenciar uma coleção de livros, utilizando MongoDB como banco de dados. A API oferece endpoints para obter, criar, atualizar e excluir livros da coleção.

## Funcionalidades

- **Obter todos os livros**: Recupera uma lista de livros armazenados na base de dados.
- **Obter livro por ID público**: Retorna os detalhes de um livro específico com base no ID público fornecido.
- **Criar um novo livro**: Adiciona um novo livro à coleção.
- **Atualizar um livro existente**: Modifica os detalhes de um livro já cadastrado.
- **Excluir um livro**: Remove um livro da coleção com base no ID público.

## Estrutura do Projeto

- **Controllers**: `LivrosController` lida com as requisições HTTP e mapeia os endpoints.
- **Data**: `MongoDbContext` gerencia a conexão com o MongoDB e o acesso à coleção de livros.
- **Models**: Define o modelo `Livro`, que representa os dados dos livros no MongoDB.
- **Repositories**: `LivroRepository` implementa os métodos de acesso ao banco de dados para a coleção de livros.
- **Testes**: Testes unitários escritos usando Moq e Xunit para validar as funcionalidades do controlador.

## Requisitos

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MongoDB](https://www.mongodb.com/try/download/community)

## Instalação

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/nome-do-repositorio.git
   cd nome-do-repositorio

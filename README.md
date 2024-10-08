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

2. **Configure o MongoDB**:
   Certifique-se de que o MongoDB esteja rodando localmente ou em um servidor acessível e adicione as configurações de conexão no arquivo appsettings.json:
   ```json
   {
     "MongoDbSettings": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "LivrosDb"
     }
   }
   
3. **Restaurar pacotes NuGet:**
   ```bash
   dotnet restore

4. **Executar a aplicação:**
    ```bash
    dotnet run

## Endpoints da API

GET /api/livros
Retorna uma lista de todos os livros.

GET /api/livros/{publicId}
Retorna um livro específico com base no ID público.

POST /api/livros
Cria um novo livro. Exemplo de corpo da requisição:

```json
{
  "titulo": "Dom Quixote",
  "autor": "Miguel de Cervantes",
  "anoPublicacao": 1605,
  "genero": "Ficção"
}
```

PUT /api/livros/{publicId}
Atualiza um livro existente. Exemplo de corpo da requisição:
```json
{
  "titulo": "Don Quixote",
  "autor": "Miguel de Cervantes",
  "anoPublicacao": 1605,
  "genero": "Ficção"
}
```
DELETE /api/livros/{publicId}
Remove um livro da coleção com base no ID público.


## Testes
Os testes unitários estão implementados no arquivo LivrosControllerTests.cs, usando Moq e Xunit para simular o repositório e validar os comportamentos do controlador.

Para rodar os testes:
```bash
dotnet test
```

## Swagger
A documentação da API é gerada automaticamente com Swagger. Após iniciar a aplicação em modo desenvolvimento, acesse o Swagger UI em:
```bash
http://localhost:5000/swagger
```

## Link do Azure
https://dev.azure.com/biancaroman1425/Projeto-CP5



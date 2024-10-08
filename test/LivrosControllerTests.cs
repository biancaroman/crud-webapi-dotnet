using LivrosApi;
using repositories;
using model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MongoDB.Bson;

namespace test
{
    public class LivrosControllerTests
    {
         // Testa se o método Get retorna uma lista de livros corretamente.
        [Fact]
        public async Task Get_ReturnsListOfLivros()
        {
            var mockRepo = new Mock<ILivroRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Livro>());

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Livro>>(okResult.Value);
        }

         // Testa se o método GetByPublicId retorna NotFound quando o ID público é inválido.
        [Fact]
        public async Task GetByPublicId_ReturnsNotFound_WhenPublicIdIsInvalid()
        {
            var mockRepo = new Mock<ILivroRepository>();
            mockRepo.Setup(repo => repo.GetByPublicIdAsync("invalid-id")).ReturnsAsync((Livro?)null);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.GetByPublicId("invalid-id");

            Assert.IsType<NotFoundResult>(result);
        }

         // Testa se o método Create retorna CreatedAtAction quando um livro é criado com sucesso.
        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenLivroIsCreated()
        {
            var mockRepo = new Mock<ILivroRepository>();
            var publicId = Guid.NewGuid().ToString();
            var livro = new Livro
            {
                Id = ObjectId.GenerateNewId(),
                PublicId = publicId,
                Titulo = "Livro Teste",
                Autor = "Autor Teste",
                AnoPublicacao = 2023,
                Genero = "Ficção"
            };
            mockRepo.Setup(repo => repo.CreateAsync(livro)).Returns(Task.CompletedTask);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Create(livro);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdLivro = Assert.IsType<Livro>(actionResult.Value);
            Assert.Equal(publicId, createdLivro.PublicId); 
        }

         // Testa se o método Update retorna NotFound quando o livro não existe.
        [Fact]
        public async Task Update_ReturnsNotFound_WhenLivroDoesNotExist()
        {
            var mockRepo = new Mock<ILivroRepository>();
            var livroId = ObjectId.GenerateNewId();
            var livro = new Livro { Id = livroId, Titulo = "Livro Teste", Autor = "Autor Teste", AnoPublicacao = 2023, Genero = "Ficção" };
            mockRepo.Setup(repo => repo.GetByPublicIdAsync("invalid-id")).ReturnsAsync((Livro?)null);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Update("invalid-id", livro);

            Assert.IsType<NotFoundResult>(result);
        }

        // Testa se o método Update retorna NoContent quando o livro é atualizado com sucesso.
        [Fact]
        public async Task Update_ReturnsNoContent_WhenLivroIsUpdated()
        {
            var mockRepo = new Mock<ILivroRepository>();
            var livroId = ObjectId.GenerateNewId();
            var livro = new Livro { Id = livroId, Titulo = "Livro Teste", Autor = "Autor Teste", AnoPublicacao = 2023, Genero = "Ficção" };
            mockRepo.Setup(repo => repo.GetByPublicIdAsync(livro.PublicId)).ReturnsAsync(livro);
            mockRepo.Setup(repo => repo.UpdateAsync(livroId, livro)).Returns(Task.CompletedTask);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Update(livro.PublicId, livro);

            Assert.IsType<NoContentResult>(result);
        }

        // Testa se o método Delete retorna NotFound quando o ID público é inválido.
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenPublicIdIsInvalid()
        {
            var mockRepo = new Mock<ILivroRepository>();
            mockRepo.Setup(repo => repo.GetByPublicIdAsync("invalid-id")).ReturnsAsync((Livro?)null);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Delete("invalid-id");

            Assert.IsType<NotFoundResult>(result);
        }

        // Testa se o método Delete retorna NoContent quando o livro é excluído com sucesso.
        [Fact]
        public async Task Delete_ReturnsNoContent_WhenLivroIsDeleted()
        {
            var mockRepo = new Mock<ILivroRepository>();
            var livroId = ObjectId.GenerateNewId();
            var livro = new Livro { Id = livroId, Titulo = "Livro Teste", Autor = "Autor Teste", AnoPublicacao = 2023, Genero = "Ficção" };
            mockRepo.Setup(repo => repo.GetByPublicIdAsync(livro.PublicId)).ReturnsAsync(livro);
            mockRepo.Setup(repo => repo.DeleteAsync(livroId)).Returns(Task.CompletedTask);

            var controller = new LivrosController(mockRepo.Object);

            var result = await controller.Delete(livro.PublicId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}

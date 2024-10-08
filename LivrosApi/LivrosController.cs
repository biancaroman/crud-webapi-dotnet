using Microsoft.AspNetCore.Mvc;
using model;
using repositories;

namespace LivrosApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        public LivrosController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        /// <summary>
        /// Obtém todos os livros.
        /// </summary>
        /// <returns>Uma lista de livros disponíveis na coleção.</returns>
        /// <remarks>
        /// Este método retorna todos os livros armazenados na base de dados.
        /// Você pode usar essa rota para visualizar todos os livros cadastrados.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<List<Livro>>> Get()
        {
            var livros = await _livroRepository.GetAllAsync();
            return Ok(livros);
        }

        /// <summary>
        /// Obtém um livro pelo ID público.
        /// </summary>
        /// <param name="publicId">O ID público do livro.</param>
        /// <returns>O livro correspondente ao ID público, se encontrado.</returns>
        /// <remarks>
        /// Use este método para buscar detalhes de um livro específico fornecendo seu ID público.
        /// Caso o ID não seja encontrado, será retornado um código de status 404.
        /// </remarks>
        [HttpGet("{publicId}")]
        public async Task<IActionResult> GetByPublicId(string publicId)
        {
            var livro = await _livroRepository.GetByPublicIdAsync(publicId);
            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        /// <summary>
        /// Cria um novo livro.
        /// </summary>
        /// <param name="livro">O livro a ser criado.</param>
        /// <returns>O livro criado.</returns>
        /// <response code="201">Retorna o livro criado.</response>
        /// <response code="400">Se o modelo estiver inválido.</response>
        /// <remarks>
        /// Para criar um livro, forneça um objeto Livro com os detalhes necessários.
        /// Se o modelo for inválido, um código de status 400 será retornado.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Livro>> Create(Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _livroRepository.CreateAsync(livro);
            return CreatedAtAction(nameof(GetByPublicId), new { publicId = livro.PublicId }, livro);
        }

        /// <summary>
        /// Atualiza um livro existente.
        /// </summary>
        /// <param name="publicId">O ID público do livro a ser atualizado.</param>
        /// <param name="livro">O livro com as atualizações.</param>
        /// <returns>Resultado da operação de atualização.</returns>
        /// <response code="204">Se a atualização for bem-sucedida.</response>
        /// <response code="404">Se o livro não for encontrado.</response>
        /// <remarks>
        /// Para atualizar um livro, forneça o ID público e um objeto Livro com as novas informações.
        /// Se o livro não for encontrado, um código de status 404 será retornado.
        /// </remarks>
        [HttpPut("{publicId}")]
        public async Task<IActionResult> Update(string publicId, Livro livro)
        {
            var existingLivro = await _livroRepository.GetByPublicIdAsync(publicId);
            if (existingLivro == null)
            {
                return NotFound();
            }

            livro.Id = existingLivro.Id; 
            await _livroRepository.UpdateAsync(existingLivro.Id, livro);
            return NoContent();
        }

        /// <summary>
        /// Exclui um livro pelo ID público.
        /// </summary>
        /// <param name="publicId">O ID público do livro a ser excluído.</param>
        /// <returns>Resultado da operação de exclusão.</returns>
        /// <response code="204">Se a exclusão for bem-sucedida.</response>
        /// <response code="404">Se o livro não for encontrado.</response>
        /// <remarks>
        /// Use este método para excluir um livro fornecendo seu ID público.
        /// Se o livro não for encontrado, um código de status 404 será retornado.
        /// </remarks>
        [HttpDelete("{publicId}")]
        public async Task<IActionResult> Delete(string publicId)
        {
            var livro = await _livroRepository.GetByPublicIdAsync(publicId);
            if (livro == null)
            {
                return NotFound();
            }

            await _livroRepository.DeleteAsync(livro.Id); 
            return NoContent();
        }
    }
}

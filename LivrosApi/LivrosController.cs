using Microsoft.AspNetCore.Mvc;
using webapi_dotnet.model;
using webapi_dotnet.repositories;

namespace webapi_dotnet.LivrosApi
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

        [HttpGet]
        public async Task<ActionResult<List<Livro>>> Get()
        {
            var livros = await _livroRepository.GetAllAsync();
            return Ok(livros);
        }

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

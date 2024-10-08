using model;
using data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly IMongoCollection<Livro> _livros;

        public LivroRepository(MongoDbContext context)
        {
            _livros = context.Livros;
        }

        public async Task<List<Livro>> GetAllAsync()
        {
            return await _livros.Find(l => true).ToListAsync();
        }

        public async Task<Livro> GetByIdAsync(ObjectId id)
        {
            return await _livros.Find(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Livro?> GetByPublicIdAsync(string publicId) 
        {
            return await _livros.Find(l => l.PublicId == publicId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Livro livro)
        {
            await _livros.InsertOneAsync(livro);
        }

        public async Task UpdateAsync(ObjectId id, Livro livro)
        {
            await _livros.ReplaceOneAsync(l => l.Id == id, livro);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _livros.DeleteOneAsync(l => l.Id == id);
        }
    }
}

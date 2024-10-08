using MongoDB.Bson;
using model;

namespace repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> GetAllAsync();
        Task<Livro> GetByIdAsync(ObjectId id);
        Task<Livro?> GetByPublicIdAsync(string publicId); 
        Task CreateAsync(Livro livro);
        Task UpdateAsync(ObjectId id, Livro livro);
        Task DeleteAsync(ObjectId id);
    }
}


using MongoDB.Bson;
using webapi_dotnet.model;

namespace webapi_dotnet.repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> GetAllAsync();
        Task<Livro> GetByIdAsync(ObjectId id);
        Task<Livro> GetByPublicIdAsync(string publicId); 
        Task CreateAsync(Livro livro);
        Task UpdateAsync(ObjectId id, Livro livro);
        Task DeleteAsync(ObjectId id);
    }
}


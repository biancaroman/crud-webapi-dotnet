using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using webapi_dotnet.model;

namespace webapi_dotnet.data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDbSettings:DatabaseName").Value);
        }

        public IMongoCollection<Livro> Livros => _database.GetCollection<Livro>("Livros");
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi_dotnet.model
{
    public class Livro
    {
        [BsonId]  
        [BsonRepresentation(BsonType.ObjectId)] 
        public ObjectId Id { get; set; }

        public string PublicId { get; set; } = Guid.NewGuid().ToString(); 
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public string? Genero { get; set; }
    }
}

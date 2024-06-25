using MedicalNotesService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalNotesService.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Note> Notes => _database.GetCollection<Note>("MedicalNotes");
    }
}
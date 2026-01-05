using MongoDB.Driver;
using MyAssessment.Models;

namespace MyAssessment.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration config)
        {
            var connectionString = config["MongoDB:ConnectionString"];
            var databaseName = config["MongoDB:DatabaseName"];
            
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Student> Students => _database.GetCollection<Student>("students");
        public IMongoCollection<Competency> Competencies => _database.GetCollection<Competency>("competencies");
        public IMongoCollection<Grade> Grades => _database.GetCollection<Grade>("grades");
        public IMongoCollection<AppSettings> Settings => _database.GetCollection<AppSettings>("settings");
        public IMongoCollection<Kelas> KelasList => _database.GetCollection<Kelas>("kelas");
        public IMongoCollection<MapelItem> MapelList => _database.GetCollection<MapelItem>("mapel");
    }
}
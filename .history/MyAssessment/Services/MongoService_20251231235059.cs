using MongoDB.Driver;
using System.Configuration;
using MyAssessment.Models;

namespace MyAssessment.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
            var databaseName = ConfigurationManager.AppSettings["DatabaseName"];
            
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Student> Students => _database.GetCollection<Student>("students");
        public IMongoCollection<Teacher> Teachers => _database.GetCollection<Teacher>("teachers");
        public IMongoCollection<Competency> Competencies => _database.GetCollection<Competency>("competencies");
        public IMongoCollection<Grade> Grades => _database.GetCollection<Grade>("grades");
    }
}
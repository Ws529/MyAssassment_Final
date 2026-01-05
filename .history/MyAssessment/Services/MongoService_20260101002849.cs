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
        public IMongoCollection<Competency> Competencies => _database.GetCollection<Competency>("competencies");
        public IMongoCollection<Grade> Grades => _database.GetCollection<Grade>("grades");
        public IMongoCollection<AppSettings> Settings => _database.GetCollection<AppSettings>("settings");

        /// <summary>
        /// Mengambil pengaturan aplikasi (dokumen tunggal)
        /// Jika belum ada, akan dibuat dengan nilai default
        /// </summary>
        public AppSettings GetAppSettings()
        {
            var settings = Settings.Find(_ => true).FirstOrDefault();
            if (settings == null)
            {
                settings = new AppSettings();
                Settings.InsertOne(settings);
            }
            return settings;
        }

        /// <summary>
        /// Menyimpan pengaturan aplikasi
        /// </summary>
        public void SaveAppSettings(AppSettings settings)
        {
            var filter = Builders<AppSettings>.Filter.Eq(s => s.Id, settings.Id);
            Settings.ReplaceOne(filter, settings, new ReplaceOptions { IsUpsert = true });
        }
    }
}
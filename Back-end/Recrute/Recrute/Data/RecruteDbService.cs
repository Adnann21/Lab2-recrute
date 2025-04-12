using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Recrute.Data
{
    public class RecruteDbService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RecruteDbService> _logger;
        private readonly IMongoDatabase? database;

        public RecruteDbService(IConfiguration configuration, ILogger<RecruteDbService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            try
            {
                var connection = _configuration.GetConnectionString("MongoDb");
                if (string.IsNullOrEmpty(connection))
                {
                    throw new ArgumentNullException(nameof(connection), "MongoDB connection string is missing or invalid.");
                }

                var mongourl = MongoUrl.Create(connection);
                var mongoclient = new MongoClient(mongourl);
                database = mongoclient.GetDatabase(mongourl.DatabaseName);

                _logger.LogInformation("MongoDB database initialized successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize MongoDB database.");
                throw;
            }
        }

        public IMongoDatabase? Database => database;
    }
}
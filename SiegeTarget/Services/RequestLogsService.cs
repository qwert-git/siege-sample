using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SiegeTarget.Models;

namespace SiegeTarget.Services
{
    public class RequestLogsService
    {
        private readonly IMongoCollection<RequestLogModel> _collection;

        public RequestLogsService(IOptions<MongoDbConfig> dbConfig)
        {
            var mongoClient = new MongoClient(dbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbConfig.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<RequestLogModel>(dbConfig.Value.CollectionName);
        }

        public async Task<List<RequestLogModel>> GetAsync() => await _collection.Find(_ => true).SortByDescending(c => c.DateTime).ToListAsync();

        public async Task<RequestLogModel?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(RequestLogModel newBook) => await _collection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, RequestLogModel updatedBook) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
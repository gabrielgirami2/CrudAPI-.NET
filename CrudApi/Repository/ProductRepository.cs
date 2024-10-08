using CrudApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAllAsync() => await _products.Find(p => true).ToListAsync();

        public async Task<Product> GetByIdAsync(string id) => await _products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) => await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) => await _products.ReplaceOneAsync(p => p.Id == id, product);

        public async Task DeleteAsync(string id) => await _products.DeleteOneAsync(p => p.Id == id);
    }
}

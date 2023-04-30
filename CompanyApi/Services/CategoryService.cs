using CompanyApi.Models.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class CategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _categoryCollection = mongoDatabase.GetCollection<Category>("category");
    }

    public async Task<List<Category>> GetAsync() =>
        await _categoryCollection.Find(_ => true).ToListAsync();

    public async Task<Category?> GetAsync(string id) =>
        await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Category newCategory) =>
        await _categoryCollection.InsertOneAsync(newCategory);

    public async Task UpdateAsync(string id, Category updatedCategory) =>
        await _categoryCollection.ReplaceOneAsync(x => x.Id == id, updatedCategory);

    public async Task RemoveAsync(string id) =>
        await _categoryCollection.DeleteOneAsync(x => x.Id == id);
}
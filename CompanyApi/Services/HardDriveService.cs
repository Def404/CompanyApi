using CompanyApi.Models.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class HardDriveService
{
    private readonly IMongoCollection<HardDrive> _hardDriveCollection;
    
    public HardDriveService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _hardDriveCollection = mongoDatabase.GetCollection<HardDrive>("hardDrive");
    }
    
    public async Task<List<HardDrive>> GetAsync() =>
        await _hardDriveCollection.Find(_ => true).ToListAsync();

    public async Task<HardDrive?> GetAsync(string id) =>
        await _hardDriveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task<List<HardDrive>> GetOfCategoryAsync(string categoryId) =>
        await _hardDriveCollection.Find(x => x.CategoryId.Equals(categoryId)).ToListAsync();
    
    public async Task<List<HardDrive>> GetOfConnectIntAsync(string connectIntTypeId) =>
        await _hardDriveCollection.Find(x => x.ConnectionIntTypeId.Equals(connectIntTypeId)).ToListAsync();

    public async Task<List<HardDrive>> GetOfKeyword(string keyword)
    {
        var filter = new BsonDocument { { "name", new BsonDocument("$regex", keyword) } };

        return await _hardDriveCollection.Find(filter).ToListAsync();
    }
    
    public async Task<List<HardDrive>> GetOfPrice(int? priceStart, int? priceEnd)
    {
        var filter = new BsonDocument { { "price", new BsonDocument{{"$gte", priceStart} , {"$lte", priceEnd} } } };

        return await _hardDriveCollection.Find(filter).ToListAsync();
    }
    
    public async Task<List<HardDrive>> GetOfSize(int size) =>
        await _hardDriveCollection.Find(x => x.Size == size).ToListAsync();

    public async Task CreateAsync(HardDrive newHardDrive) =>
        await _hardDriveCollection.InsertOneAsync(newHardDrive);

    public async Task UpdateAsync(string id, HardDrive updatedHardDrive) =>
        await _hardDriveCollection.ReplaceOneAsync(x => x.Id == id, updatedHardDrive);

    public async Task RemoveAsync(string id) =>
        await _hardDriveCollection.DeleteOneAsync(x => x.Id == id);
}
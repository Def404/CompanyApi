using CompanyApi.Models.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class HardDriveService
{
    private readonly IMongoCollection<HardDriveM> _hardDriveCollection;
    
    public HardDriveService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _hardDriveCollection = mongoDatabase.GetCollection<HardDriveM>("hardDrive");
    }
    
    public async Task<ActionResult<IEnumerable<HardDriveM>>> GetAsync() =>
        await _hardDriveCollection.Find(_ => true).ToListAsync();

    public async Task<HardDriveM?> GetAsync(string id) =>
        await _hardDriveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task<List<HardDriveM>> GetOfCategoryAsync(string categoryId) =>
        await _hardDriveCollection.Find(x => x.CategoryId.Equals(categoryId)).ToListAsync();
    
    public async Task<List<HardDriveM>> GetOfConnectIntAsync(string connectIntTypeId) =>
        await _hardDriveCollection.Find(x => x.ConnectionIntTypeId.Equals(connectIntTypeId)).ToListAsync();

    public async Task<List<HardDriveM>> GetOfKeyword(string keyword)
    {
        var filter = new BsonDocument { { "name", new BsonDocument("$regex", keyword) } };

        return await _hardDriveCollection.Find(filter).ToListAsync();
    }
    
    public async Task<List<HardDriveM>> GetOfPrice(int? priceStart, int? priceEnd)
    {
        var filter = new BsonDocument { { "price", new BsonDocument{{"$gte", priceStart} , {"$lte", priceEnd} } } };

        return await _hardDriveCollection.Find(filter).ToListAsync();
    }
    
    public async Task<List<HardDriveM>> GetOfSize(int size) =>
        await _hardDriveCollection.Find(x => x.Size == size).ToListAsync();

    public async Task CreateAsync(HardDriveM newHardDriveM) =>
        await _hardDriveCollection.InsertOneAsync(newHardDriveM);

    public async Task UpdateAsync(string id, HardDriveM updatedHardDriveM) =>
        await _hardDriveCollection.ReplaceOneAsync(x => x.Id == id, updatedHardDriveM);

    public async Task RemoveAsync(int ps_id) =>
        await _hardDriveCollection.DeleteOneAsync(x => x.ps_id == ps_id);
}
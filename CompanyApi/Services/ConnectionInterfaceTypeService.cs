using CompanyApi.Models.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class ConnectionInterfaceTypeService
{
    private readonly IMongoCollection<ConnectionInterfaceType> _connectionIntTypeCollection;

    public ConnectionInterfaceTypeService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _connectionIntTypeCollection = mongoDatabase.GetCollection<ConnectionInterfaceType>("connectionInterfaceType");
    }
    
    public async Task<List<ConnectionInterfaceType>> GetAsync() =>
        await _connectionIntTypeCollection.Find(_ => true).ToListAsync();

    public async Task<ConnectionInterfaceType?> GetAsync(int ps_id) =>
        await _connectionIntTypeCollection.Find(x => x.ps_id == ps_id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(ConnectionInterfaceType newInterfaceType) =>
        await _connectionIntTypeCollection.InsertOneAsync(newInterfaceType);

    public async Task UpdateAsync(string id, ConnectionInterfaceType updatedInterfaceType) =>
        await _connectionIntTypeCollection.ReplaceOneAsync(x => x.Id == id, updatedInterfaceType);

    public async Task RemoveAsync(string id) =>
        await _connectionIntTypeCollection.DeleteOneAsync(x => x.Id == id);
}
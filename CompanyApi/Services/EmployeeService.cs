using CompanyApi.Models.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class EmployeeService
{
    private readonly IMongoCollection<EmployeeM> _employeeCollection;

    public EmployeeService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _employeeCollection = mongoDatabase.GetCollection<EmployeeM>("employee");
    }

    public async Task<List<EmployeeM>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

    public async Task<EmployeeM?> GetAsync(string id) =>
        await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(EmployeeM newEmployeeM) =>
        await _employeeCollection.InsertOneAsync(newEmployeeM);

    public async Task UpdateAsync(string id, EmployeeM updatedEmployeeM) =>
        await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployeeM);

    public async Task RemoveAsync(string login) =>
        await _employeeCollection.DeleteOneAsync(x => x.Login == login);
}
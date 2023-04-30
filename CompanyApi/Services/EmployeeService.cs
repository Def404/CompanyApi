using CompanyApi.Models.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CompanyApi.Services;

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public EmployeeService(IOptions<CompanyShopDbSettings> companyShopDbSet)
    {
        var mongoClient = new MongoClient(companyShopDbSet.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(companyShopDbSet.Value.DatabaseName);
        _employeeCollection = mongoDatabase.GetCollection<Employee>("employee");
    }

    public async Task<List<Employee>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

    public async Task<Employee?> GetAsync(string id) =>
        await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Employee newEmployee) =>
        await _employeeCollection.InsertOneAsync(newEmployee);

    public async Task UpdateAsync(string id, Employee updatedEmployee) =>
        await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

    public async Task RemoveAsync(string id) =>
        await _employeeCollection.DeleteOneAsync(x => x.Id == id);
}
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VendorHub.Domain.Vendors;
using VendorHub.Infrastructure.Utility;

namespace VendorHub.Infrastructure.Persistence;

public class VendorDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IOptions<MongoDbSettings> _options;

    public VendorDbContext(IOptions<MongoDbSettings> options)
    {
        var connectionString = options.Value.ConnectionURI;
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
        _options = options;
    }

    public IMongoCollection<Vendor> Vendors => _database.GetCollection<Vendor>(_options.Value.CollectionName);
}


using AutoFixture;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VendorHub.IntegrationTests.Utility
{
    internal static class MongoDbConfig
    {
        private static Fixture _fixture = new Fixture();
        internal static List<Vendor> seedVendors;

        internal static void InitializeDbForTests(WebApplicationFactory<Program> appFactory)
        {
            using var scope = appFactory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VendorDbContext>();
            db.Vendors.DeleteMany(Builders<Vendor>.Filter.Empty);
            db.Vendors.InsertMany(GenerateSeedVendors());
        }

        internal static List<Vendor> GenerateSeedVendors()
        {
            var productFixtures = _fixture.Build<Vendor>()
                                          .Without(p => p.Id)
                                          .Do(x => x.Id = ObjectId.GenerateNewId().ToString())
                                          .With(v => v.Name, _fixture
                                          .Create<string>()
                                          .Replace("-", ""))
                                          .CreateMany(10)
                                          .ToList();

            var products = new List<Vendor>();
            products.AddRange(productFixtures);
            seedVendors = products;
            return products;
        }
    }
}

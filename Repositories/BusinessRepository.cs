using BusinessManagement.Configuration;
using BusinessManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BusinessManagement.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IMongoCollection<Business> _businessCollection;

        public BusinessRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _businessCollection = mongoDatabase.GetCollection<Business>(
                mongoDbSettings.Value.BusinessCollectionName
            );
        }

        // ─── Insert ─────────────────────────────────────────────────────────────

        public async Task<Business> InsertBusinessAsync(Business business)
        {
            await _businessCollection.InsertOneAsync(business);
            return business;
        }

        // ─── Fetch All (excluding soft-deleted) ─────────────────────────────────

        public async Task<List<Business>> FetchAllBusinessesAsync()
        {
            var filter = Builders<Business>.Filter.Eq(b => b.IsDeleted, false);
            return await _businessCollection.Find(filter).ToListAsync();
        }

        // ─── Fetch By BusinessId ─────────────────────────────────────────────────

        public async Task<Business?> FetchBusinessByIdAsync(string businessId)
        {
            var filter = Builders<Business>.Filter.And(
                Builders<Business>.Filter.Eq(b => b.BusinessId, businessId),
                Builders<Business>.Filter.Eq(b => b.IsDeleted, false)
            );
            return await _businessCollection.Find(filter).FirstOrDefaultAsync();
        }

        // ─── Update ──────────────────────────────────────────────────────────────

        public async Task<bool> UpdateBusinessAsync(string businessId, Business updatedBusiness)
        {
            var filter = Builders<Business>.Filter.And(
                Builders<Business>.Filter.Eq(b => b.BusinessId, businessId),
                Builders<Business>.Filter.Eq(b => b.IsDeleted, false)
            );

            var updateDefinition = Builders<Business>.Update
                .Set(b => b.BusinessName, updatedBusiness.BusinessName)
                .Set(b => b.CreatorId, updatedBusiness.CreatorId)
                .Set(b => b.CreatorName, updatedBusiness.CreatorName)
                .Set(b => b.UpdatedDate, updatedBusiness.UpdatedDate);

            var result = await _businessCollection.UpdateOneAsync(filter, updateDefinition);
            return result.ModifiedCount > 0;
        }

        // ─── Soft Delete ─────────────────────────────────────────────────────────

        public async Task<bool> SoftDeleteBusinessAsync(string businessId)
        {
            var filter = Builders<Business>.Filter.And(
                Builders<Business>.Filter.Eq(b => b.BusinessId, businessId),
                Builders<Business>.Filter.Eq(b => b.IsDeleted, false)
            );

            var updateDefinition = Builders<Business>.Update
                .Set(b => b.IsDeleted, true)
                .Set(b => b.UpdatedDate, DateTime.UtcNow);

            var result = await _businessCollection.UpdateOneAsync(filter, updateDefinition);
            return result.ModifiedCount > 0;
        }

        // ─── Existence Check ─────────────────────────────────────────────────────

        public async Task<bool> BusinessExistsAsync(string businessId)
        {
            var filter = Builders<Business>.Filter.And(
                Builders<Business>.Filter.Eq(b => b.BusinessId, businessId),
                Builders<Business>.Filter.Eq(b => b.IsDeleted, false)
            );
            return await _businessCollection.Find(filter).AnyAsync();
        }
    }
}

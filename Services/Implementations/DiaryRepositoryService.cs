using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Nyneo_Web.DataAccess;
using Nyneo_Web.Models;

namespace Nyneo_Web.Services.Implementations;

public class DiaryRepositoryService : IDiaryRepository
{
    private readonly IMongoCollection<Diary> _diarysCollection;

    public DiaryRepositoryService(
        IOptions<DiaryDatabaseSettings> diaryDatabaseSettings)
    {
        var ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        var mongoClient = new MongoClient(ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            diaryDatabaseSettings.Value.DatabaseName);

        _diarysCollection = mongoDatabase.GetCollection<Diary>(
            diaryDatabaseSettings.Value.DiariesCollectionName);
    }

    // ------------------------
    public async Task<IEnumerable<Diary>> GetAll()
    {
        return await _diarysCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Diary?> GetById(string diaryId)
    {
        return await _diarysCollection.Find(x => x.Id == diaryId).FirstOrDefaultAsync();
    }


    public async Task AddAsync(Diary newDiary) =>
      await _diarysCollection.InsertOneAsync(newDiary);


    public async Task UpdateAsync(Diary model)
    {
        await _diarysCollection.ReplaceOneAsync(d => d.Id == model.Id, model);
    }

    public async Task DeleteAsync(string diaryId)
    {
        await _diarysCollection.DeleteOneAsync(d => d.Id == diaryId);
    }
}
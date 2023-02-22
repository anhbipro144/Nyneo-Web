using BookStoreMVC.Services;
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
        var mongoClient = new MongoClient(
            diaryDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            diaryDatabaseSettings.Value.DatabaseName);

        _diarysCollection = mongoDatabase.GetCollection<Diary>(
            diaryDatabaseSettings.Value.DiariesCollectionName);
    }



    public async Task<Diary?> GetAsync(string id) =>
        await _diarysCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Diary newDiary) =>
        await _diarysCollection.InsertOneAsync(newDiary);

    public async Task UpdateAsync(string id, Diary updatedDiary) =>
        await _diarysCollection.ReplaceOneAsync(x => x.Id == id, updatedDiary);

    public async Task RemoveAsync(string id) =>
        await _diarysCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<IEnumerable<Diary>> GetAll()
    {
        return await _diarysCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Diary?> GetById(string diaryId)
    {
        return await _diarysCollection.Find(x => x.Id == diaryId).FirstOrDefaultAsync();
    }

    public Task AddAsync(Diary model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Diary model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string diaryId)
    {
        throw new NotImplementedException();
    }
}
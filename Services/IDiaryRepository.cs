
using Nyneo_Web.Models;

namespace BookStoreMVC.Services;

public interface IDiaryRepository
{
    Task<IEnumerable<Diary>> GetAll();
    Task<Diary?> GetById(string diaryId);

    Task AddAsync(Diary model);

    Task UpdateAsync(Diary model);

    Task DeleteAsync(string diaryId);
}
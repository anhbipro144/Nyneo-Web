namespace Nyneo_Web.DataAccess;

public class DiaryDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string DiariesCollectionName { get; set; } = null!;
    public string CommentsCollectionName { get; set; } = null!;
}
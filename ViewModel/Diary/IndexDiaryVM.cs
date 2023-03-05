using System.ComponentModel;

namespace Nyneo_Web.ViewModel;

public class IndexDiaryVM
{

    public string? id { get; set; }
    public string? userName { get; set; }


    [DisplayName("Title")]
    public string? title { get; set; }


    [DisplayName("Content")]
    public string? content { get; set; }

    public DateTime created_at { get; set; }

    public string? signedUrl { get; set; }

    public string? userId { get; set; }

}


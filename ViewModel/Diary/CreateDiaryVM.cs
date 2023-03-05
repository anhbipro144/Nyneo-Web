using System.ComponentModel;

namespace Nyneo_Web.ViewModel;

public class CreateDiary
{
    public string? userId { get; set; }


    [DisplayName("Title")]
    public string? title { get; set; }
    [DisplayName("Content")]
    public string? content { get; set; }


    [DisplayName("Image File")]
    // [permittedextensions(new string[] { ".jpg", "png" }]
    public IFormFile? ImgFile { get; set; }



}


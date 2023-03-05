using System.ComponentModel;

namespace Nyneo_Web.ViewModel;

public class UpdateDiaryVM
{
    public string? id { get; set; }
    public string? userId { get; set; }


    [DisplayName("Title")]
    public string? title { get; set; }
    [DisplayName("Content")]
    public string? content { get; set; }

    public string? savedImgName { get; set; }
    public string? signedUrl { get; set; }


    [DisplayName("Image File")]
    // [permittedextensions(new string[] { ".jpg", "png" }]
    public IFormFile? ImgFile { get; set; }



}


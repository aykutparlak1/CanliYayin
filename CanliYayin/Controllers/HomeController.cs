using CanliYayin.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;


    public HomeController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;

    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public JsonResult GetVideos()
    {
        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

        var mp4Files = Directory.EnumerateFiles(uploadsFolder, "*.mp4")
            .Select(file => new Chanels
            {
                Title = Path.GetFileName(file),
                LinkUrl = Path.Combine("/uploads", Path.GetFileName(file))
            })
            .ToList();
        List<Chanels> _channels = new List<Chanels>
        {
            new Chanels { Title = "TRT Haber", LinkUrl = "https://tv-trthaber.medya.trt.com.tr/master_720.m3u8" },
        };

        var combinedList = _channels.Concat(mp4Files).ToList();

        return Json(combinedList);
    }
}

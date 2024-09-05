using CanliYayin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CanliYayin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private List<Chanels> _channels = new List<Chanels>
        {
            new Chanels { Title = "TRT Haber", LinkUrl = "https://tv-trthaber.medya.trt.com.tr/master_720.m3u8" },
        };

        public IActionResult Index()
        {
            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            var mp4Files = Directory.EnumerateFiles(uploadsFolder, "*.mp4")
                                    .Select(file => new Chanels
                                    {
                                        Title = Path.GetFileName(file),
                                        LinkUrl = Path.Combine("/uploads", Path.GetFileName(file))
                                    })
                                    .ToList();

            var combinedList = _channels.Concat(mp4Files).ToList();

            return View(combinedList);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace PixApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHostingEnvironment _env;
        public IndexModel(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetImage(int width=300, int height = 200,string file= "bloom.jpg")
        {       

            var fileName =Path.Combine( _env.ContentRootPath ,"Images",file);

            Stream imageStream = new MemoryStream();

            using (Image<Rgba32> image = Image.Load(fileName))
            {
                image.Mutate(x => x
                     .Resize(width,height));

                image.Save(imageStream, ImageFormats.Jpeg);
            }

            imageStream.Position = 0;

            return new FileStreamResult(imageStream, "image/jpeg");
        }

        
    }
}
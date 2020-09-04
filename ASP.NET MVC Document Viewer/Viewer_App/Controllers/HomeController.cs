using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Viewer_App.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            License lic = new License();
            lic.SetLicense(@"D:/GD Licenses/Conholdate.Total.NET.lic");
            MemoryStream outputStream = new MemoryStream();
            //specify just the file name if you are pulling the data from database
            var filePath = Server.MapPath("~/Content/");
            string fileName = filePath + "sample.pdf";

            FileType fileType = FileType.FromExtension(Path.GetExtension(fileName));

            using (Viewer viewer = new Viewer(() => GetSourceFileStream(fileName), () => new LoadOptions(fileType)))
            {
                HtmlViewOptions Options = HtmlViewOptions.ForEmbeddedResources(
                    (pageNumber) => outputStream,
                    (pageNumber, pageStream) => { });
                viewer.View(Options);
            }

            outputStream.Position = 0;
             

            return File(outputStream, "text/html");
             
        }
        private Stream GetSourceFileStream(string fileName) =>
            new MemoryStream(GetSourceFileBytesFromDb(fileName));

        //TODO: If you want to pull the data from the DB
        private byte[] GetSourceFileBytesFromDb(string fileName) =>
            System.IO.File.ReadAllBytes(fileName);
    }
}
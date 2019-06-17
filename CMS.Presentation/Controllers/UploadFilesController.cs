
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CMS.Presentation.Controllers
{
    public class UploadFilesController : ApiController
    {
        [Route("api/FileAPI/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Content/Images/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Save the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".jpg";
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, "/Content/Images/" + fileName);
        }

    }
}
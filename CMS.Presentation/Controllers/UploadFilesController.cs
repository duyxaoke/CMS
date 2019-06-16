using Core.DTO.Response;
using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CMS.Presentation.Controllers
{
    public class UploadFilesController : Controller
    {
        [HttpPost]
        public ActionResult UploadImage()
        {
            ResponseWrapper<UploadRes> response = new ResponseWrapper<UploadRes>();
            if (!User.Identity.IsAuthenticated)
            {
                Response.StatusCode = 401;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            var httpRequest = HttpContext.Request;
            byte[] fileContents = null;
            httpRequest.InputStream.Position = 0;
            fileContents = new byte[httpRequest.ContentLength];
            httpRequest.InputStream.Read(fileContents, 0, httpRequest.ContentLength);
            if (httpRequest.Files.Count == 0 && (fileContents == null || fileContents.Length == 0))
            {
                response.Code = (int)System.Net.HttpStatusCode.BadRequest;
            }
            else
            {
                response = saveFile(httpRequest,fileContents);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private ResponseWrapper<UploadRes> saveFile(HttpRequestBase httpRequest, byte[] fileContents)
        {
            ResponseWrapper<UploadRes> response = new ResponseWrapper<UploadRes>();
            string FileName = string.Empty, strExtension = string.Empty, FileNameSaved = string.Empty;
            string PathFull = Server.MapPath("~/Content/Images/");
            if (httpRequest.Files.Count > 0)
            {
                var myFile = httpRequest.Files[0];
                FileName = myFile.FileName;
                strExtension = Path.GetExtension(FileName);
                FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                var strPathFile = Path.Combine(PathFull, FileNameSaved);

                if (myFile != null && myFile.ContentLength != 0)
                {
                    if (!Directory.Exists(PathFull))
                        Directory.CreateDirectory(PathFull);
                    myFile.SaveAs(strPathFile);
                    response.Code = (int)System.Net.HttpStatusCode.OK;
                    response.Success = true;
                    response.Data = new UploadRes() { CodeStep = "finish", PathFile = PathFull + FileNameSaved };
                }
            }
            else if (fileContents != null || fileContents.Length > 0)
            {
                FileName = httpRequest.Headers["FileName"];
               strExtension = Path.GetExtension(FileName);
                if (!Directory.Exists(PathFull))
                    Directory.CreateDirectory(PathFull);

                FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                string strPathFile = Path.Combine(PathFull, FileNameSaved);
                System.IO.File.WriteAllBytes(strPathFile, fileContents);
                response.Code = (int)System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Data = new UploadRes() { CodeStep = "finish", PathFile = PathFull + FileNameSaved };
            }
            return response;
        }


        public class UploadRes
        {
            public string CodeStep { get; set; }
            public string PathFile { get; set; }
        }

        public class UploadImageRes
        {
            public int statusCode { get; set; }
            public string errorMessage { get; set; }
            public List<UploadItemRes> data { get; set; }
        }
        public class UploadItemRes
        {
            public string userTitle { get; set; }
            public string systemTitle { get; set; }
            public string rePath { get; set; }
            public string abPath { get; set; }
        }

    }
}
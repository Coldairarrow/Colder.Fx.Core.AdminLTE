using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Coldairarrow.Web.Controllers
{
    public class DemoController : BaseController
    {
        #region 视图

        public ActionResult UMEditor()
        {
            return View();
        }

        public ActionResult UploadFileIndex()
        {
            return View();
        }
        public ActionResult UploadFileForm()
        {
            return View();
        }

        #endregion

        #region 接口

        public ActionResult UploadFile(string fileBase64, string fileName)
        {
            byte[] bytes = fileBase64.ToBytes_FromBase64Str();
            string fileDir = Path.Combine(GlobalSwitch.WebRootPath, "Upload", "File");
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);
            string filePath = Path.Combine(fileDir, fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (MemoryStream m = new MemoryStream(bytes))
                {
                    m.WriteTo(fileStream);
                }
            }

            return Success();
        }

        #endregion
    }
}
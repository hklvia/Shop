using COMMON;
using Constant;
using Img.Models;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace Img.Controllers
{
    public class ImgController : Controller
    {
        public ActionResult Upload()
        {
            //List<string> fileNames = new List<string>();
            List<ImgInfo> fileList = new List<ImgInfo>();
            var files = Request.Files;   //["Img"]
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file == null)
                {
                    return Json(new { state = false, msg = "图片不存在，请重新上传" });
                }
                byte[] bytes = Common.StreamToBytes(file.InputStream);
                var fileExtension = FileHelper.CheckTextFile(bytes);

                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传的文件已损坏" });
                }
                if (!(fileExtension == FileExtension.WEBP || fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传的类型有误" });
                }
                //DateTime dt = DateTime.Now;
                Random random = new Random();
                int randomNum = random.Next(0, 99999);
                var extension = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid().ToString("N") + randomNum.ToString() + extension;

                var imgType = Request["imgType"];
                string upLoadPath;
                if (WebConstants.ImgPathDicts.ContainsKey(imgType))
                {
                    upLoadPath = WebConstants.ImgPathDicts[imgType];
                }
                else
                {
                    return Json(new { state = false });
                }
                string fullPath = upLoadPath + newFileName;
                file.SaveAs(Server.MapPath(fullPath));

                //将图片上传到云服务器（七牛云）
                Mac mac = new Mac(ConfigurationManager.AppSettings["AccessKey"], ConfigurationManager.AppSettings["SecretKey"]);
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = ConfigurationManager.AppSettings["Bucket"];
                string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

                Config config = new Config();
                // 空间对应的机房
                config.Zone = Zone.ZONE_CN_North;
                // 是否使用https域名
                config.UseHttps = false;
                // 上传是否使用cdn加速
                config.UseCdnDomains = true;

                FormUploader fu = new FormUploader(config);
                HttpResult result = fu.UploadData(bytes, fullPath, token, null);

                ImgInfo imgInfo = new ImgInfo()
                {
                    Url = "http://localhost:50069/" + fullPath,
                    CloudUrl = "http://shop.statics.hklvia.top/" + fullPath
                };
                fileList.Add(imgInfo);
            }
            return Json(fileList);
        }
    }
}
using System;

namespace COMMON
{
    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        PNG = 13780,
        WEBP = 8273,
        VALIDFILE = 9999999,
        // 255216 jpg; 
        // 7173 gif; 
        //bmp = 6677,
        // 13780 png; 
        //swf = 6787,
        //exe = 7790,
        //dll = 7790,
        //rar = 8297,
        //zip = 8075,
        //7z = 55122,
        //xml = 6063,
        //html = 6033,
        //aspx = 239187,
        //cs = 117115,
        //js = 119105,
        //txt = 102100,
        //sql = 255254,
    }
    public class FileHelper
    {
        public static FileExtension CheckTextFile(byte[] bytes)
        {
            //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //System.IO.BinaryReader br = new System.IO.BinaryReader(fileStream);
            string fileType = string.Empty;
            try
            {
                byte data = bytes[0];
                fileType += data.ToString();
                data = bytes[1];
                fileType += data.ToString();
                FileExtension extension;
                try
                {
                    extension = (FileExtension)Enum.Parse(typeof(FileExtension), fileType);
                }
                catch
                {
                    extension = FileExtension.VALIDFILE;
                }
                return extension;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Collections;
using EMEWE.CarManagement.Commons;

namespace EMEWE.CarManagement.Commons.CommonClass
{
    public static class ImageFile
    {
        /// <summary>
        /// WebClient上传文件至服务器
        /// </summary>
        /// <param name="fileNameFullPath">要上传的文件（全路径格式）</param>
        /// <param name="strUrlDirPath">Web服务器文件夹路径</param>
        /// <returns>True/False是否上传成功</returns>
        public static string UpLoadFile(string fileNameFullPath, string strUrlDirPath)
        {
            try
            {
                string CreateFolderPath = @"" + strUrlDirPath;
                if (!Directory.Exists(CreateFolderPath))
                {
                    Directory.CreateDirectory(CreateFolderPath);
                }
                //得到要上传的文件文件名
                string fileName = fileNameFullPath.Substring(fileNameFullPath.LastIndexOf("\\") + 1);
                //新文件名由年月日时分秒及毫秒组成
                //得到文件扩展名
                string fileNameExt = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (!string.IsNullOrEmpty(fileNameExt))
                {
                    //保存在服务器上时，将文件改名
                   string  picName=DateTime.Now.Month.ToString()+DateTime.Now.Second.ToString()+ fileName;
                   strUrlDirPath = strUrlDirPath + picName;
                    // 创建WebClient实例
                    WebClient myWebClient = new WebClient();
                    myWebClient.Credentials = CredentialCache.DefaultCredentials;
                    // 将要上传的文件打开读进文件流
                    FileStream myFileStream = new FileStream(fileNameFullPath, FileMode.Open, FileAccess.Read);
                    BinaryReader myBinaryReader = new BinaryReader(myFileStream);

                    byte[] postArray = myBinaryReader.ReadBytes((int)myFileStream.Length);
                    //打开远程Web地址，将文件流写入
                    Stream postStream = myWebClient.OpenWrite(strUrlDirPath, "PUT");
                    if (postStream.CanWrite)
                    {
                        postStream.Write(postArray, 0, postArray.Length);
                        return strUrlDirPath;
                        
                    }

                    postStream.Close();//关闭流
                    myWebClient.Dispose();
                    myFileStream.Close();
                    myBinaryReader.Close();

                }
            }
            catch (Exception exp)
            {
                return "";
                
            }
            return "";
        }
        /// <summary>
        /// 复制图片
        /// </summary>
        /// <param name="filepath1">原图片路径（全路径)</param>
        /// <param name="filepath2">新路径</param>
        public static void Copt(string filepath1, string filepath2)
        {
            FileInfo file = new FileInfo(filepath1);
            file.CopyTo(filepath2, true);
        }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public static DateTime t = DateTime.Now;

    }
}

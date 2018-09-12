using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Collections;


namespace EMEWEQUALITY.HelpClass
{

    public static class ImageFile
    {
        /// <summary>
        /// WebClient上传文件至服务器
        /// </summary>
        /// <param name="fileNameFullPath">要上传的文件（全路径格式）</param>
        /// <param name="strUrlDirPath">Web服务器文件夹路径</param>
        /// <returns>True/False是否上传成功</returns>
        public static bool UpLoadFile(string fileNameFullPath, string strUrlDirPath)
        {
            bool rbool = false;
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
                    strUrlDirPath = strUrlDirPath + fileName;
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
                        rbool = true;
                    }

                    postStream.Close();//关闭流
                    myWebClient.Dispose();
                    myFileStream.Close();
                    myBinaryReader.Close();


                }
            }
            catch (Exception exp)
            {

            }
            return rbool;
        }

        /// <summary>
        /// 下载服务器文件至客户端
        /// </summary>
        /// <param name="strUrlFilePath">要下载的Web服务器上的文件地址（全路径)
        /// <param name="Dir">下载到的目录（存放位置，本地机器文件夹）</param>
        /// <returns>True/False是否上传成功</returns>
        public static bool DownLoadFile(string strUrlFilePath, string strLocalDirPath)
        {

            // 创建WebClient实例
            WebClient client = new WebClient();

            //被下载的文件名
            string fileName = strUrlFilePath.Substring(strUrlFilePath.LastIndexOf("\\"));

            //另存为的绝对路径＋文件名
            string Path = strLocalDirPath + fileName;

            try
            {
                WebRequest myWebRequest = WebRequest.Create(strUrlFilePath);
            }
            catch (Exception exp)
            {

            }
            try
            {
                client.DownloadFile(strUrlFilePath, Path);
                return true;
            }
            catch (Exception exp)
            {

                return false;
            }

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
        /// 删除图片
        /// </summary>
        /// <param name="path">图片路径</param>
        public static void Delete(string path)
        {

            try
            {

                File.Delete(path);
            }
            catch (Exception ex)
            {

            }

            //}
        }
        /// <summary>
        /// 移动图片
        /// </summary>
        /// <param name="path1">原图片路径（全路径)</param>
        /// <param name="path2">新路径(完成的路径包括新名称)</param>
        public static void MoveImage(string path1, string path2)
        {
            File.Move(path1, path2);
        }
        public static ArrayList listWarning = new ArrayList();//异常信息存放








    }
}

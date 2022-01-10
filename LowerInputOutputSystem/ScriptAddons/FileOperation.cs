using System;
using System.IO;

namespace LowerInputOutputSystem.ScriptAddons
{
    public class FileOperation
    {
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="msg">信息</param>
        public static void WriteFile(string path, string msg)
        {
            StreamWriter streamWriter;
            FileInfo fileInfo = new FileInfo(path);
            streamWriter = fileInfo.CreateText();
            streamWriter.WriteLine(msg);
            streamWriter.Close();
            streamWriter.Dispose();
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            StreamReader streamReader;
            FileInfo fileInfo = new FileInfo(path);
            streamReader = fileInfo.OpenText();
            string msg = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            return msg;
        }
    }
}
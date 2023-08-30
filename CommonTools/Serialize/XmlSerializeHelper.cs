using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CommonTools.Serialize
{
    public static class XmlSerializeHelper
    {

        /// <summary>
        /// 把对象使用XML格式写入本地文件
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj">对象</param>
        /// <param name="filePath">文件地址</param>
        /// <param name="extension">文件后缀名，默认为.xml</param>
        /// <returns>错误信息，没有错误则为空</returns>
        public static string WriteFile<TObject>(TObject obj, string filePath, string extension = ".xml")
            where TObject : class
        {
            try
            {
                if (obj == null)
                    return "对象参数为null";

                var dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var path = Path.ChangeExtension(filePath, extension);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                using Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                serializer.Serialize(stream, obj);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 读取文件并反序列化
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="filePath">文件地址</param>
        /// <param name="extension">文件后缀名</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>反序列化得到的对象</returns>
        public static TObject? ReadFile<TObject>(string filePath, string extension, out string errorMsg)
            where TObject : class
        {
            TObject? t = null;
            errorMsg = string.Empty;

            try
            {
                var path = string.IsNullOrEmpty(extension) ? filePath : Path.ChangeExtension(filePath, extension);

                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TObject));

                    using Stream stream = new FileStream(path, FileMode.Open);
                    var obj = serializer.Deserialize(stream);
                    t = obj as TObject;
                }
                else
                    errorMsg = "文件路径不存在";

                if (t == null)
                    errorMsg = "反序列化失败";

                return t;
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
                return t;
            }
        }

    }
}

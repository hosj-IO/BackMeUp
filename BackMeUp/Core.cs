using System;
using System.IO;
using System.Xml.Serialization;

namespace BackMeUp
{
    public class Core
    {
        public static void SerializeConfig(object serializableObject, Type type, string fileName)
        {
            var filePath = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonDocuments) + "\\BackMeUp\\" + fileName;

            CheckIfFileExistAndCreateIfNot(filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(type);

                xmlSerializer.Serialize(fileStream, serializableObject);
            }
        }

        public static object DeserializeConfig(Type type, string fileName)
        {
            var filePath = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonDocuments) + "\\BackMeUp\\" + fileName;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(type);
                    var returnValue = xmlSerializer.Deserialize(fileStream);
                    return returnValue;
                }
            }
            catch
            {
                //throw ex;
            }
            return null;
        }

        private static void CheckIfFileExistAndCreateIfNot(string path)
        {
            if (path == null) throw new ArgumentException("Path was not valid");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (File.AppendText(path)) { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace File_System
{
    public class FileSystem
    {
        public static void ReadFromXml<T>(ref T Object, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamReader reader = new StreamReader(@filePath))
            {
                Object = (T)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        public static void WriteToXml<T>(ref T Object, string filePath)
        {
            XmlSerializer writer;

            using (StreamWriter file = new StreamWriter(@filePath))
            {
                writer = new XmlSerializer(typeof(T));
                writer.Serialize(file, Object);

                file.Close();
            }
        }

        public static void ReadFromBinary<T>(ref T Object, string filePath)
        {
            using (Stream stream = File.Open(@filePath, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                Object = (T)bin.Deserialize(stream);
            }
        }

        public static void WriteToBinary<T>(ref T Object, string filePath)
        {
            using (Stream stream = File.Open(@filePath, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, Object);
            }
        }
    }
}

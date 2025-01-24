using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Moveta.Intern
{
    public static class TD60
    {
        public static void SerializeToXml<T>(List<T> list, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, list);
            }
        }

        public static T DeserializeFromXml<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }


        public class Systables
        {
            public string Creator { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int Colcount { get; set; }
            public string System { get; set; }
            public int SNUM { get; set; }
            public string Remarks { get; set; }
            public string Label { get; set; }
        }

        public class Syscolumns
        {
            public string Name { get; set; }
            public string TBName { get; set; }
            public string TBCreator { get; set; }
            public int Colno { get; set; }
            public string Coltype { get; set; }
            public int Length { get; set; }
            public int Scale { get; set; }
            public string Nulls { get; set; }
            public string Remarks { get; set; }
            public string Label { get; set; }

        }
    }
}

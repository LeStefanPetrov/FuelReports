using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace FuelReports.Deserialization
{
    public class Deserializer<T>
    {
        public static List<T> Deserialize(string path)
        {
            List<T> petrolStations = new List<T>();
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Directory does not exist!");
            else
            {
                foreach (string fileName in Directory.GetFiles(path, "*.xml"))
                {
                    using (var reader = new StreamReader(fileName))
                        petrolStations.Add((T)serializer.Deserialize(reader));
                }
            }
            return petrolStations;
        }
    }
}

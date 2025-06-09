using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Restauracja
{
    [Serializable]
    public struct OrderedProducts
    {
        [XmlElement("ProductName")]
        public string productName;

        [XmlElement("Price")]
        public decimal price;

        [XmlElement("TableNumber")]
        public uint tableNr;
    }


    public static class OrderedProductsXmlSerializer
    {
        // Serializacja listy obiektów do pliku XML
        public static void SerializeListToXml(List<OrderedProducts> products, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<OrderedProducts>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, products);
            }
        }

        // Deserializacja listy obiektów z pliku XML
        public static List<OrderedProducts> DeserializeListFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<OrderedProducts>));
            using (var reader = new StreamReader(filePath))
            {
                return (List<OrderedProducts>)serializer.Deserialize(reader);
            }
        }
    }


}

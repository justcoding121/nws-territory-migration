using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sandbox.Program;
using System.Xml.Serialization;
using System.Xml;

namespace Sandbox.Helpers;

public class XmlHelper
{
    public static string Serialize<T>(T request) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));

        using var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);

        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true });
        serializer.Serialize(xmlWriter, request);

        return stringWriter.ToString();
    }

    public static T? Deserialize<T>(string responseXml) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));

        using TextReader reader = new StringReader(responseXml);
        return serializer.Deserialize(reader) as T;
    }
}

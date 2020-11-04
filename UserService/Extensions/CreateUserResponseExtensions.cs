using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UserService.Extensions
{
    public static class CreateUserResponseExtensions
    {
        public static string ToXml<T>(this T value) where T : new()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(value.GetType());
            var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
            var xml = "";
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw, settings))
                {
                    serializer.Serialize(writer, value, ns);
                    xml = sw.ToString();
                }
            }

            return xml;
        }
    }
}

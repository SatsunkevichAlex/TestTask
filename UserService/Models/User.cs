using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Xml.Serialization;
using UserService.Enums;

namespace UserService.Models
{
    [Serializable]
    public class User
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus Status { get; set; }
    }
}

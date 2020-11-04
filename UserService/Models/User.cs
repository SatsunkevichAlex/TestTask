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
        public UserStatus Status { get; set; }
    }
}

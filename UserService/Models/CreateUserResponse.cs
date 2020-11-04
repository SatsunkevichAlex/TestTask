using System;
using System.Xml.Serialization;

namespace UserService.Models
{
    [Serializable]
    [XmlRoot("Response")]
    public class CreateUserResponse
    {
        [XmlAttribute]
        public bool Success { get; set; }
        [XmlAttribute]
        public int ErrorId { get; set; }
        [XmlElement]
        public User User { get; set; }
    }
}

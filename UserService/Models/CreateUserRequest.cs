using System;
using System.Xml.Serialization;

namespace UserService.Models
{
    [Serializable]
    [XmlRoot("Request")]
    public class CreateUserRequest
    {
        [XmlElement(ElementName = "User")]
        public User User { get; set; }        
    }
}

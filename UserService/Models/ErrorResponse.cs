﻿using System;
using System.Xml.Serialization;

namespace UserService.Models
{
    [Serializable]
    [XmlRoot("Response")]
    public class ErrorResponse
    {
        [XmlAttribute]
        public bool Success{ get; set; }
        [XmlAttribute]
        public int ErrorId { get; set; }
        [XmlElement(ElementName = "ErrorMsg")]
        public string ErrorMessage { get; set; }
    }
}

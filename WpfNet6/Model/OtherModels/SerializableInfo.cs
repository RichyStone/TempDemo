using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfNet6.Model.OtherModels
{
    [Serializable]
    public class SerializableInfo
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IP { get; set; } = string.Empty;

        public int Port { get; set; }

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        [XmlIgnore]
        public string PostalCode { get; set; } = string.Empty;
    }
}

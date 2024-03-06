using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ExportDtos
{
    [XmlType("Property")]
    public class ExportProperyDto
    {
        [XmlAttribute("postal-code")]
        public string PostalCode { get; set; }

        [XmlElement("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; }

        [XmlElement("Area")]
        public int Area { get; set; }

        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }
    }
}

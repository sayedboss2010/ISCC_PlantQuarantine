using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlantQuar.DTO.HelperClasses
{
public      class XMLToClass<T> where T : class
    {
        public    object ConvertXMLToClass(string xml_formate, string xml_Data)
        {
            if (!String.IsNullOrEmpty(xml_formate))
            {
                xml_Data= "<"+ xml_formate + ">" + xml_Data + "</"+ xml_formate + ">";
            }
            var serializer = new XmlSerializer(typeof(T));
            var buffer = Encoding.UTF8.GetBytes(xml_Data);
            using (var stream = new MemoryStream(buffer))
            {
                var conversion = (T)serializer.Deserialize(stream);
                return conversion;
            }
        }
    }
}

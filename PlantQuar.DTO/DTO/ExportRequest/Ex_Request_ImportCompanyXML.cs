using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Ex_Request_ImportCompanyXML
    {

        private Ex_Request_ImportCompanyXMLEx_Request_ImportCompany[] ex_Request_ImportCompanyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Ex_Request_ImportCompany")]
        public Ex_Request_ImportCompanyXMLEx_Request_ImportCompany[] Ex_Request_ImportCompany
        {
            get
            {
                return this.ex_Request_ImportCompanyField;
            }
            set
            {
                this.ex_Request_ImportCompanyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Ex_Request_ImportCompanyXMLEx_Request_ImportCompany
    {

        private string importCompanyField;

        private string imporeterCompanyAddressField;

        private string reciever_NameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImportCompany
        {
            get
            {
                return this.importCompanyField;
            }
            set
            {
                this.importCompanyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImporeterCompanyAddress
        {
            get
            {
                return this.imporeterCompanyAddressField;
            }
            set
            {
                this.imporeterCompanyAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Reciever_Name
        {
            get
            {
                return this.reciever_NameField;
            }
            set
            {
                this.reciever_NameField = value;
            }
        }
    }    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Treatments_XmlDTO
    {

        private Treatments_XmlDTOTreatments[] treatmentsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("@treatments")]
        public Treatments_XmlDTOTreatments[] treatments
        {
            get
            {
                return this.treatmentsField;
            }
            set
            {
                this.treatmentsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Treatments_XmlDTOTreatments
    {

        private System.DateTime user_Creation_DateField;

        private string Ar_NameMethodField;

        private string Ar_NameTreatField;
        private string En_NameMethodField;

        private string En_NameTreatField;

        private decimal theDoseField;

        private decimal temperatureField;

        private byte exposure_DayField;

        private byte exposure_HourField;

        private byte exposure_MinuteField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime User_Creation_Date
        {
            get
            {
                return this.user_Creation_DateField;
            }
            set
            {
                this.user_Creation_DateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Ar_NameMethod
        {
            get
            {
                return this.Ar_NameMethodField;
            }
            set
            {
                this.Ar_NameMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Ar_NameTreat
        {
            get
            {
                return this.Ar_NameTreatField;
            }
            set
            {
                this.Ar_NameTreatField = value;
            }
        }

        /// <remarks/>
        /// [System.Xml.Serialization.XmlAttributeAttribute()]
        public string En_NameMethod
        {
            get
            {
                return this.En_NameMethodField;
            }
            set
            {
                this.En_NameMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string En_NameTreat
        {
            get
            {
                return this.En_NameTreatField;
            }
            set
            {
                this.En_NameTreatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TheDose
        {
            get
            {
                return this.theDoseField;
            }
            set
            {
                this.theDoseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Temperature
        {
            get
            {
                return this.temperatureField;
            }
            set
            {
                this.temperatureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Exposure_Day
        {
            get
            {
                return this.exposure_DayField;
            }
            set
            {
                this.exposure_DayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Exposure_Hour
        {
            get
            {
                return this.exposure_HourField;
            }
            set
            {
                this.exposure_HourField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Exposure_Minute
        {
            get
            {
                return this.exposure_MinuteField;
            }
            set
            {
                this.exposure_MinuteField = value;
            }
        }
    }


}

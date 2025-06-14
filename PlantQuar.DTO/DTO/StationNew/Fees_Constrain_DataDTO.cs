using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.StationNew
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Fees_Constrain_DataDTO
    {

        private Fees_Constrain_DataDTOFT[] ftField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ft")]
        public Fees_Constrain_DataDTOFT[] ft
        {
            get
            {
                return this.ftField;
            }
            set
            {
                this.ftField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Fees_Constrain_DataDTOFT
    {

        private Fees_Constrain_DataDTOFTEcrf ecrfField;

        private string name_ArField;

        private byte isTreatmentField;

        /// <remarks/>
        public Fees_Constrain_DataDTOFTEcrf ecrf
        {
            get
            {
                return this.ecrfField;
            }
            set
            {
                this.ecrfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name_Ar
        {
            get
            {
                return this.name_ArField;
            }
            set
            {
                this.name_ArField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte IsTreatment
        {
            get
            {
                return this.isTreatmentField;
            }
            set
            {
                this.isTreatmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Fees_Constrain_DataDTOFTEcrf
    {

        private decimal fees_AmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Fees_Amount
        {
            get
            {
                return this.fees_AmountField;
            }
            set
            {
                this.fees_AmountField = value;
            }
        }
    }
}

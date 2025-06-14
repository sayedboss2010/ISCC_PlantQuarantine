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
    public partial class committeeEmployee_XmlDTO
    {


        private committeeEmployee_XmlDTOEmployees[] employeesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("@employees")]
        public committeeEmployee_XmlDTOEmployees[] employees
        {
            get
            {
                return this.employeesField;
            }
            set
            {
                this.employeesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class committeeEmployee_XmlDTOEmployees
    {

        private long employee_IdField;

        private long employee_noField;

        private string employee_nameField;

        private long committee_idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long Employee_Id
        {
            get
            {
                return this.employee_IdField;
            }
            set
            {
                this.employee_IdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long employee_no
        {
            get
            {
                return this.employee_noField;
            }
            set
            {
                this.employee_noField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string employee_name
        {
            get
            {
                return this.employee_nameField;
            }
            set
            {
                this.employee_nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long committee_id
        {
            get
            {
                return this.committee_idField;
            }
            set
            {
                this.committee_idField = value;
            }
        }
    }


}

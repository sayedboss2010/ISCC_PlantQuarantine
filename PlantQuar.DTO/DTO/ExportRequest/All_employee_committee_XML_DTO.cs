
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class All_employee_committee_XML_DTO
{

    private All_employee_committee_XML_DTOFn_CommitteEmployee_GetData[] fn_CommitteEmployee_GetDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("fn_CommitteEmployee_GetData")]
    public All_employee_committee_XML_DTOFn_CommitteEmployee_GetData[] fn_CommitteEmployee_GetData
    {
        get
        {
            return this.fn_CommitteEmployee_GetDataField;
        }
        set
        {
            this.fn_CommitteEmployee_GetDataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class All_employee_committee_XML_DTOFn_CommitteEmployee_GetData
{

    //private ushort employee_IdField;

    private string fullNameField;

    //private string loginNameField;

    //private byte passwordField;

    private bool iSAdminField;

    //private string empTokenField;

    /// <remarks/>
    //[System.Xml.Serialization.XmlAttributeAttribute()]
    //public ushort Employee_Id
    //{
    //    get
    //    {
    //        return this.employee_IdField;
    //    }
    //    set
    //    {
    //        this.employee_IdField = value;
    //    }
    //}

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string FullName
    {
        get
        {
            return this.fullNameField;
        }
        set
        {
            this.fullNameField = value;
        }
    }

    /// <remarks/>
    //[System.Xml.Serialization.XmlAttributeAttribute()]
    //public string LoginName
    //{
    //    get
    //    {
    //        return this.loginNameField;
    //    }
    //    set
    //    {
    //        this.loginNameField = value;
    //    }
    //}

    /// <remarks/>
    //[System.Xml.Serialization.XmlAttributeAttribute()]
    //public byte Password
    //{
    //    get
    //    {
    //        return this.passwordField;
    //    }
    //    set
    //    {
    //        this.passwordField = value;
    //    }
    //}

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool ISAdmin
    {
        get
        {
            return this.iSAdminField;
        }
        set
        {
            this.iSAdminField = value;
        }
    }

    /// <remarks/>
    //[System.Xml.Serialization.XmlAttributeAttribute()]
    //public string EmpToken
    //{
    //    get
    //    {
    //        return this.empTokenField;
    //    }
    //    set
    //    {
    //        this.empTokenField = value;
    //    }
    //}
}

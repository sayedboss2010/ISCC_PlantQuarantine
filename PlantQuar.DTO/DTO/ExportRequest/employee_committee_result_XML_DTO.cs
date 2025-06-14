
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class employee_committee_result_XML_DTO
{

    private employee_committee_result_XML_DTOFn_Get_Employee_Committee_Result[] fn_Get_Employee_Committee_ResultField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("fn_Get_Employee_Committee_Result")]
    public employee_committee_result_XML_DTOFn_Get_Employee_Committee_Result[] fn_Get_Employee_Committee_Result
    {
        get
        {
            return this.fn_Get_Employee_Committee_ResultField;
        }
        set
        {
            this.fn_Get_Employee_Committee_ResultField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class employee_committee_result_XML_DTOFn_Get_Employee_Committee_Result
{

    private string resultField;

    private string employee_nameField;

    private string notesField;

    private bool resultbitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string result
    {
        get
        {
            return this.resultField;
        }
        set
        {
            this.resultField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Employee_name
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
    public string notes
    {
        get
        {
            return this.notesField;
        }
        set
        {
            this.notesField = value;
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool resultbit
    {
        get
        {
            return this.resultbitField;
        }
        set
        {
            this.resultbitField = value;
        }
    }
}



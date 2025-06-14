
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Ex_Request_FeesXMLDTO
{

    private Ex_Request_FeesXMLDTOFn_GetRequest_Fees[] fn_GetRequest_FeesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("fn_GetRequest_Fees")]
    public Ex_Request_FeesXMLDTOFn_GetRequest_Fees[] fn_GetRequest_Fees
    {
        get
        {
            return this.fn_GetRequest_FeesField;
        }
        set
        {
            this.fn_GetRequest_FeesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class Ex_Request_FeesXMLDTOFn_GetRequest_Fees
{

    private byte ferssTypeIdField;

    private string feesTypeField;

    private decimal amountField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte ferssTypeId
    {
        get
        {
            return this.ferssTypeIdField;
        }
        set
        {
            this.ferssTypeIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string feesType
    {
        get
        {
            return this.feesTypeField;
        }
        set
        {
            this.feesTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Amount
    {
        get
        {
            return this.amountField;
        }
        set
        {
            this.amountField = value;
        }
    }
}


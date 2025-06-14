
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Payment_DataDTO
{

    private Payment_DataDTOEip_Payment[] eip_PaymentField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("eip_Payment")]
    public Payment_DataDTOEip_Payment[] eip_Payment
    {
        get
        {
            return this.eip_PaymentField;
        }
        set
        {
            this.eip_PaymentField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class Payment_DataDTOEip_Payment
{

    private byte iS_OnlineOfflineField;

    private decimal amountField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte IS_OnlineOffline
    {
        get
        {
            return this.iS_OnlineOfflineField;
        }
        set
        {
            this.iS_OnlineOfflineField = value;
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


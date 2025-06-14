
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class AttachmentData_Xml
{

    private AttachmentData_XmlA_AttachmentData[] a_AttachmentDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("A_AttachmentData")]
    public AttachmentData_XmlA_AttachmentData[] A_AttachmentData
    {
        get
        {
            return this.a_AttachmentDataField;
        }
        set
        {
            this.a_AttachmentDataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AttachmentData_XmlA_AttachmentData
{

    private string attachmentPathField;

    private string attachment_NumberField;

    private string attachment_TypeNameField;

    private System.DateTime startDateField;

    private System.DateTime endDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string AttachmentPath
    {
        get
        {
            return this.attachmentPathField;
        }
        set
        {
            this.attachmentPathField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Attachment_Number
    {
        get
        {
            return this.attachment_NumberField;
        }
        set
        {
            this.attachment_NumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Attachment_TypeName
    {
        get
        {
            return this.attachment_TypeNameField;
        }
        set
        {
            this.attachment_TypeNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime StartDate
    {
        get
        {
            return this.startDateField;
        }
        set
        {
            this.startDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime EndDate
    {
        get
        {
            return this.endDateField;
        }
        set
        {
            this.endDateField = value;
        }
    }
}


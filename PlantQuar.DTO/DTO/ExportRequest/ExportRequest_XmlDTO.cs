
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ExportRequest_XmlDTO
{
    private ExportRequest_XmlDTOItem_Data[] item_DataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("@Item_Data")]
    public ExportRequest_XmlDTOItem_Data[] Item_Data
    {
        get
        {
            return this.item_DataField;
        }
        set
        {
            this.item_DataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ExportRequest_XmlDTOItem_Data
{

    private ExportRequest_XmlDTOItem_DataTemp_table_Lot[] lot_DataField;

    private ExportRequest_XmlDTOItem_DataTemp_table_Constrain[] constrain_DataField;

    //fz for android
    private long Request_Item_IDField;

    //fz byte to int
    private int item_numberField;

    private int isExportField;

    private int item_TypeField;

    //fz ushort to int
    private int item_IdField;

    private string item_NameField;

    private string scientific_NameField;

    private string item_Cat_NameField;
    private string item_Name_enField;

    private string scientific_Name_enField;

    private string item_Cat_Name_enField;

    private string itemPartTypeNameField;

    private string item_StrainField;

    private string itemStatusField;
    private string itemPartTypeName_enField;

    private string item_Strain_enField;

    private string itemStatus_enField;

    private int itemStatus_IDField;

    private string itemPurposeField;

    private string item_ShortNameField;
    private string itemPurpose_enField;

    private string item_ShortName_enField;

    private int purpose_IDField;
    //fz ushort to int
    private int plantCat_IDField;

    private int plantPart_IDField;
    //fz ushort to int
    private int plantPartTypeField;
    private string HSCODEField;
    private decimal grossWeightField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("@temp_table_Lot", IsNullable = false)]
    public ExportRequest_XmlDTOItem_DataTemp_table_Lot[] Lot_Data
    {
        get
        {
            return this.lot_DataField;
        }
        set
        {
            this.lot_DataField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("@temp_table_Constrain", IsNullable = false)]
    public ExportRequest_XmlDTOItem_DataTemp_table_Constrain[] Constrain_Data
    {
        get
        {
            return this.constrain_DataField;
        }
        set
        {
            this.constrain_DataField = value;
        }
    }


    //fz for android
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public long Request_Item_ID
    {
        get
        {
            return this.Request_Item_IDField;
        }
        set
        {
            this.Request_Item_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Item_number
    {
        get
        {
            return this.item_numberField;
        }
        set
        {
            this.item_numberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IsExport
    {
        get
        {
            return this.isExportField;
        }
        set
        {
            this.isExportField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Item_Type
    {
        get
        {
            return this.item_TypeField;
        }
        set
        {
            this.item_TypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Item_Id
    {
        get
        {
            return this.item_IdField;
        }
        set
        {
            this.item_IdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Name
    {
        get
        {
            return this.item_NameField;
        }
        set
        {
            this.item_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Scientific_Name
    {
        get
        {
            return this.scientific_NameField;
        }
        set
        {
            this.scientific_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Cat_Name
    {
        get
        {
            return this.item_Cat_NameField;
        }
        set
        {
            this.item_Cat_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemPartTypeName
    {
        get
        {
            return this.itemPartTypeNameField;
        }
        set
        {
            this.itemPartTypeNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Strain
    {
        get
        {
            return this.item_StrainField;
        }
        set
        {
            this.item_StrainField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemStatus
    {
        get
        {
            return this.itemStatusField;
        }
        set
        {
            this.itemStatusField = value;
        }
    }
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Name_en
    {
        get
        {
            return this.item_Name_enField;
        }
        set
        {
            this.item_Name_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Scientific_Name_en
    {
        get
        {
            return this.scientific_Name_enField;
        }
        set
        {
            this.scientific_Name_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Cat_Name_en
    {
        get
        {
            return this.item_Cat_Name_enField;
        }
        set
        {
            this.item_Cat_Name_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemPartTypeName_en
    {
        get
        {
            return this.itemPartTypeName_enField;
        }
        set
        {
            this.itemPartTypeName_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_Strain_en
    {
        get
        {
            return this.item_Strain_enField;
        }
        set
        {
            this.item_Strain_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemStatus_en
    {
        get
        {
            return this.itemStatus_enField;
        }
        set
        {
            this.itemStatus_enField = value;
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int ItemStatus_ID
    {
        get
        {
            return this.itemStatus_IDField;
        }
        set
        {
            this.itemStatus_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemPurpose
    {
        get
        {
            return this.itemPurposeField;
        }
        set
        {
            this.itemPurposeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_ShortName
    {
        get
        {
            return this.item_ShortNameField;
        }
        set
        {
            this.item_ShortNameField = value;
        }
    }

    /// <remarks/>
    /// [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ItemPurpose_en
    {
        get
        {
            return this.itemPurpose_enField;
        }
        set
        {
            this.itemPurpose_enField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Item_ShortName_en
    {
        get
        {
            return this.item_ShortName_enField;
        }
        set
        {
            this.item_ShortName_enField = value;
        }
    }
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Purpose_ID
    {
        get
        {
            return this.purpose_IDField;
        }
        set
        {
            this.purpose_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int PlantCat_ID
    {
        get
        {
            return this.plantCat_IDField;
        }
        set
        {
            this.plantCat_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int plantPart_ID
    {
        get
        {
            return this.plantPart_IDField;
        }
        set
        {
            this.plantPart_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int plantPartType
    {
        get
        {
            return this.plantPartTypeField;
        }
        set
        {
            this.plantPartTypeField = value;
        }
    }
    [System.Xml.Serialization.XmlAttributeAttribute()]

    public string HSCODE
    {
        get
        {
            return this.HSCODEField;
        }
        set
        {
            this.HSCODEField = value;
        }
    }
    [System.Xml.Serialization.XmlAttributeAttribute()]

    public decimal grossWeight
    {
        get
        {
            return this.grossWeightField;
        }
        set
        {
            this.grossWeightField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ExportRequest_XmlDTOItem_DataTemp_table_Lot
{


    private string lot_NumberField;

    private int package_CountField;

    private decimal net_WeightField;

    private decimal gross_WeightField;

    private int isAcceptedField;

    private string package_Material_NameField;

    private string package_Type_NameField;

    private string farm_NameField;

    private string sampleBarcodeField;

    private int item_numberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Lot_Number
    {
        get
        {
            return this.lot_NumberField;
        }
        set
        {
            this.lot_NumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Package_Count
    {
        get
        {
            return this.package_CountField;
        }
        set
        {
            this.package_CountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Net_Weight
    {
        get
        {
            return this.net_WeightField;
        }
        set
        {
            this.net_WeightField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Gross_Weight
    {
        get
        {
            return this.gross_WeightField;
        }
        set
        {
            this.gross_WeightField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IsAccepted
    {
        get
        {
            return this.isAcceptedField;
        }
        set
        {
            this.isAcceptedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Package_Material_Name
    {
        get
        {
            return this.package_Material_NameField;
        }
        set
        {
            this.package_Material_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Package_Type_Name
    {
        get
        {
            return this.package_Type_NameField;
        }
        set
        {
            this.package_Type_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Farm_Name
    {
        get
        {
            return this.farm_NameField;
        }
        set
        {
            this.farm_NameField = value;
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string sampleBarcode
    {
        get
        {
            return this.sampleBarcodeField;
        }
        set
        {
            this.sampleBarcodeField = value;
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Item_number
    {
        get
        {
            return this.item_numberField;
        }
        set
        {
            this.item_numberField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ExportRequest_XmlDTOItem_DataTemp_table_Constrain
{

    private string constrainText_ArField;

    private string countryConstrain_TypeField;

    private int constrainOwner_IDField;

    private string union_NameField;

    private int isAnalysisField;

    private int isTreatmentField;

    private int count_allField;

    private int item_numberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ConstrainText_Ar
    {
        get
        {
            return this.constrainText_ArField;
        }
        set
        {
            this.constrainText_ArField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string CountryConstrain_Type
    {
        get
        {
            return this.countryConstrain_TypeField;
        }
        set
        {
            this.countryConstrain_TypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int ConstrainOwner_ID
    {
        get
        {
            return this.constrainOwner_IDField;
        }
        set
        {
            this.constrainOwner_IDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string union_Name
    {
        get
        {
            return this.union_NameField;
        }
        set
        {
            this.union_NameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IsAnalysis
    {
        get
        {
            return this.isAnalysisField;
        }
        set
        {
            this.isAnalysisField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IsTreatment
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

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int count_all
    {
        get
        {
            return this.count_allField;
        }
        set
        {
            this.count_allField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Item_number
    {
        get
        {
            return this.item_numberField;
        }
        set
        {
            this.item_numberField = value;
        }
    }
}


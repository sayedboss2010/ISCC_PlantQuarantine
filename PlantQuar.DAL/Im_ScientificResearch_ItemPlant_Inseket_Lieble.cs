//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantQuar.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Im_ScientificResearch_ItemPlant_Inseket_Lieble
    {
        public long ID { get; set; }
        public long Im_ScientificResearch_ID { get; set; }
        public string Scientific_Name { get; set; }
        public Nullable<short> LiableItems_Strain_Id { get; set; }
        public Nullable<int> LiableItems_Status_Id { get; set; }
        public short Package_Type_Id { get; set; }
        public string Procedure_Summery { get; set; }
        public int Research_Type_Id { get; set; }
        public int Biological_Phase_id { get; set; }
    
        public virtual A_SystemCode A_SystemCode { get; set; }
        public virtual Biological_Phase Biological_Phase { get; set; }
        public virtual Im_ScientificResearch Im_ScientificResearch { get; set; }
        public virtual LiableItems_Status LiableItems_Status { get; set; }
        public virtual ItemCategories_Type ItemCategories_Type { get; set; }
        public virtual Package_Type Package_Type { get; set; }
    }
}

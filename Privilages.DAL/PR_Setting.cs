//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Privilages.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PR_Setting
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public Nullable<int> SettingValue { get; set; }
        public bool IsRange { get; set; }
        public Nullable<int> SettingMinValue { get; set; }
        public Nullable<int> SettingMaxValue { get; set; }
        public Nullable<int> PR_ModuleId { get; set; }
    
        public virtual PR_Module PR_Module { get; set; }
    }
}

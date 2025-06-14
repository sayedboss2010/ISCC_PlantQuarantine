using System;

namespace PlantQuar.DTO.HelperClasses
{
    public class CustomOption
    {
        public int? Value { get; set; }
        public bool? Value2 { get; set; }
        public string DisplayText { get; set; }
        //add startdate and enddate to use in stationactivity index to compare dates of اعتمادات بالنشاط
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
    }
}
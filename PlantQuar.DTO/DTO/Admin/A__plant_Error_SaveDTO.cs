namespace PlantQuar.DTO.DTO.Admin
{
    public class A__plant_Error_SaveDTO
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string ErrorMessage { get; set; }
        public System.DateTime Date { get; set; }
        public string FunctionName { get; set; }
        public string User_Ip { get; set; }
        public bool IsWeb { get; set; }
    }
}
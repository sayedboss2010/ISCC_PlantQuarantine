using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Fees
{
    public class Im_Fees_ActionsDTO
    {
        public long Id { get; set; }
        public short Item_Status_ID { get; set; }
        public string ShortName_Ar { get; set; }
        public string item_Name_Ar { get; set; }
        public Nullable<bool>   Is_ImportTaxFree { get; set; }
        public string ShortName_En { get; set; }
        public string QualitativeGroup { get; set; }

        public Nullable<int> Id_Check { get; set; }

        public Nullable<decimal> Fees { get; set; }
        public Nullable<decimal> Fees_Import { get; set; }
        public Nullable<decimal> Fees_Tranzet { get; set; }
        public Nullable<int> Type { get; set; }

    }
}

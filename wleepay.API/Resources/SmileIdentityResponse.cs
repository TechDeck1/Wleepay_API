using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class SmileIdentityResponse
    {
        public Actions Actions { get; set; }
        public string ConfidenceValue { get; set; }
        public PartnerParams PartnerParams { get; set; }
        public string ResultCode { get; set; }
        public string ResultText { get; set; }
        public string SmileJobID { get; set; }
        public string Source { get; set; }
        public string sec_key { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Actions
    {
        public string Human_Review_Compare { get; set; }
        public string Human_Review_Liveness_Check { get; set; }
        public string Human_Review_Update_Selfie { get; set; }
        public string Liveness_Check { get; set; }
        public string Register_Selfie { get; set; }
        public string Return_Personal_Info { get; set; }
        public string Selfie_Provided { get; set; }
        public string Selfie_To_ID_Authority_Compare { get; set; }
        public string Selfie_To_ID_Card_Compare { get; set; }
        public string Selfie_To_Registered_Selfie_Compare { get; set; }
        public string Update_Registered_Selfie_On_File { get; set; }
        public string Verify_ID_Number { get; set; }
    }

    public class PartnerParams
    {
        public string job_id { get; set; }
        public string job_type { get; set; }
        public string more_optional_info { get; set; }
        public string optional_info { get; set; }
        public string user_id { get; set; }
    }
}

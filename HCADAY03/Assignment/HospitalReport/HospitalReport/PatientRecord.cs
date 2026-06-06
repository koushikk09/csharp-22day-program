using System;

namespace HospitalReport
{
    public class PatientRecord
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal BillAmount { get; set; }
        public string Status { get; set; }
    }
}
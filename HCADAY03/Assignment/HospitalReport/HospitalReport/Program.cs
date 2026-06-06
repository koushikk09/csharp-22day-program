using System;
using System.Collections.Generic;

namespace HospitalReport
{
    class Program
    {
        static void Main(string[] args)
        {
            // ✅ Create List with Dummy Data
            List<PatientRecord> patients = new List<PatientRecord>()
            {
                new PatientRecord { Name = "John Doe", Department = "General", BillAmount = 500, Status = "Discharged" },
                new PatientRecord { Name = "Jane Smith", Department = "Dental", BillAmount = 1200, Status = "Admitted" },
                new PatientRecord { Name = "Bob Brown", Department = "General", BillAmount = 400, Status = "Discharged" },
                new PatientRecord { Name = "Alice W.", Department = "Orthopedics", BillAmount = 2500, Status = "Admitted" },
                new PatientRecord { Name = "Sam K.", Department = "Dental", BillAmount = 800, Status = "Discharged" }
            };

            // ✅ Calculations
            int totalPatients = patients.Count;
            decimal totalRevenue = 0;

            // Department counters
            int generalCount = 0;
            int dentalCount = 0;
            int orthoCount = 0;

            foreach (var p in patients)
            {
                totalRevenue += p.BillAmount;

                if (p.Department == "General")
                    generalCount++;
                else if (p.Department == "Dental")
                    dentalCount++;
                else if (p.Department == "Orthopedics")
                    orthoCount++;
            }

            // ✅ Display Report
            PrintReport(patients, totalPatients, totalRevenue, generalCount, dentalCount, orthoCount);
        }

        static void PrintReport(List<PatientRecord> patients, int totalPatients, decimal totalRevenue,
                                int general, int dental, int ortho)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       DAILY HOSPITAL ACTIVITY REPORT            ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}\n");

            Console.WriteLine("Patient List:");

            int index = 1;
            foreach (var p in patients)
            {
                Console.WriteLine($"{index}. {p.Name,-10} - {p.Department,-12} - ₹{p.BillAmount}");
                index++;
            }

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("SUMMARY STATISTICS");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Total Patients Visited:  {totalPatients}");
            Console.WriteLine($"Total Revenue:           ₹{totalRevenue}\n");

            Console.WriteLine("Traffic by Department:");
            Console.WriteLine($"- General:       {general}");
            Console.WriteLine($"- Dental:        {dental}");
            Console.WriteLine($"- Orthopedics:   {ortho}");

            Console.WriteLine("\nEnd of Report.");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
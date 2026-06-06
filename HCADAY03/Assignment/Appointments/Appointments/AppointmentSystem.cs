using System;
using System.Collections.Generic;

class AppointmentSystem
{
    static void Main(string[] args)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       APPOINTMENT BOOKING SYSTEM                 ");
        Console.WriteLine("--------------------------------------------------");

        // Step 1: Patient Name
        Console.Write("Enter Patient Name: ");
        string patientName = Console.ReadLine();

        // Departments
        List<string> departments = new List<string>()
        {
            "General Medicine",
            "Dental",
            "Orthopedics"
        };

        // Doctors Data
        Dictionary<int, List<string>> doctors = new Dictionary<int, List<string>>()
        {
            {1, new List<string> { "Dr. A. Kumar", "Dr. B. Singh" }},
            {2, new List<string> { "Dr. C. Roy", "Dr. D. Gupta" }},
            {3, new List<string> { "Dr. E. Mehta", "Dr. F. Verma" }}
        };

        // Time slots
        List<string> timeSlots = new List<string>()
        {
            "10:00 AM",
            "11:00 AM",
            "12:00 PM"
        };

        int deptChoice = 0;

        // Step 2: Department Selection (Validation)
        while (true)
        {
            Console.WriteLine("\nSelect Department:");
            for (int i = 0; i < departments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {departments[i]}");
            }

            Console.Write("Enter Choice: ");

            if (int.TryParse(Console.ReadLine(), out deptChoice) &&
                deptChoice >= 1 && deptChoice <= departments.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Error: Invalid department selection.");
            }
        }

        string selectedDepartment = departments[deptChoice - 1];

        // Step 3: Doctor Selection
        int docChoice = 0;

        while (true)
        {
            Console.WriteLine("\nSelect Doctor:");
            var docList = doctors[deptChoice];

            for (int i = 0; i < docList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {docList[i]}");
            }

            Console.Write("Enter Choice: ");

            if (int.TryParse(Console.ReadLine(), out docChoice) &&
                docChoice >= 1 && docChoice <= docList.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Error: Invalid doctor selection.");
            }
        }

        string selectedDoctor = doctors[deptChoice][docChoice - 1];

        // Step 4: Time Slot Selection
        int timeChoice = 0;

        while (true)
        {
            Console.WriteLine("\nSelect Time Slot:");
            for (int i = 0; i < timeSlots.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {timeSlots[i]}");
            }

            Console.Write("Enter Choice: ");

            if (int.TryParse(Console.ReadLine(), out timeChoice) &&
                timeChoice >= 1 && timeChoice <= timeSlots.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Error: Invalid time slot selection.");
            }
        }

        string selectedTime = timeSlots[timeChoice - 1];

        // Create Appointment Object
        Appointment appointment = new Appointment()
        {
            PatientName = patientName,
            Department = selectedDepartment,
            Doctor = selectedDoctor,
            TimeSlot = selectedTime
        };

        Console.WriteLine("\n[Booking Confirmed]\n");

        // Display Ticket
        PrintTicket(appointment);
    }

    static void PrintTicket(Appointment appt)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("            APPOINTMENT TICKET                    ");
        Console.WriteLine("--------------------------------------------------");

        Console.WriteLine($"Patient:    {appt.PatientName}");
        Console.WriteLine($"Department: {appt.Department}");
        Console.WriteLine($"Doctor:     {appt.Doctor}");
        Console.WriteLine($"Time:       {appt.TimeSlot}");
        Console.WriteLine("Status:     Confirmed\n");

        Console.WriteLine("Please arrive 15 mins before your slot.");
        Console.WriteLine("--------------------------------------------------");
    }
}
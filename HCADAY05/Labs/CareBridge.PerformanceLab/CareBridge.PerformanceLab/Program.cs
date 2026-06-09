using CareBridge.PerformanceLab.Models;
using CareBridge.PerformanceLab.Models1;
using var db = new CareBridgeContext();
while (true)
{
    Console.Clear();
    Console.WriteLine("=================================");
    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
    Console.WriteLine("=================================");
    Console.WriteLine("1. View Patient");
    Console.WriteLine("2. View Patient Encounters");
    Console.WriteLine("3. Exit");
    Console.WriteLine();

    Console.Write("Choose Option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowPatient();
            break;

        case "2":
            ShowEncounters();
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
static void ShowPatient()
{
    using var db = new CareBridgeContext();
    var patient =
        db.Patients
          .FirstOrDefault(p => p.Mrn == "MRN999999");

    if (patient == null)
    {
        Console.WriteLine("Patient not found.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("PATIENT DETAILS");
    Console.WriteLine("----------------------------");

    Console.WriteLine($"Patient Id : {patient.PatientId}");
    Console.WriteLine($"MRN        : {patient.Mrn}");
    Console.WriteLine($"Name       : {patient.FullName}");
    Console.WriteLine($"City       : {patient.City}");
    Console.WriteLine($"Active     : {patient.IsActive}");
}
static void ShowEncounters()
{
    using var db = new CareBridgeContext();
    var patient =
        db.Patients
          .FirstOrDefault(p => p.Mrn == "MRN999999");

    if (patient == null)
    {
        Console.WriteLine("Patient not found.");
        return;
    }

    var encounters =
        db.Encounters
          .Where(e => e.PatientId == patient.PatientId)
          .ToList();

    Console.WriteLine();
    Console.WriteLine("PATIENT ENCOUNTERS");
    Console.WriteLine("----------------------------");

    Console.WriteLine($"Patient Name    : {patient.FullName}");
    Console.WriteLine($"Encounter Count : {encounters.Count}");
}

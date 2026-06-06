using System;

class VitalSignsMonitor
{
    static void Main(string[] args)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       VITAL SIGNS MONITOR                       ");
        Console.WriteLine("--------------------------------------------------");

        // Patient Name
        Console.Write("Enter Patient Name: ");
        string name = Console.ReadLine();

        // Temperature Input
        double temperature = ReadDouble("Enter Temperature (C): ", 30, 45);

        // Oxygen Input
        int oxygen = ReadInt("Enter Oxygen Level (%): ", 0, 100);

        // Pulse Input
        int pulse = ReadInt("Enter Pulse Rate (BPM): ", 30, 200);

        Console.WriteLine("\n[Analyzing Data...]\n");

        string status = CheckStatus(temperature, oxygen, pulse);

        // Report Output
        PrintReport(name, temperature, oxygen, pulse, status);
    }

    // ✅ Method to safely read double
    static double ReadDouble(string message, double min, double max)
    {
        while (true)
        {
            Console.Write(message);
            try
            {
                double value = Convert.ToDouble(Console.ReadLine());

                if (value >= min && value <= max)
                    return value;
                else
                    Console.WriteLine($"Error: Value must be between {min} and {max}.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid numeric value.");
            }
        }
    }

    // ✅ Method to safely read int
    static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            try
            {
                int value = Convert.ToInt32(Console.ReadLine());

                if (value >= min && value <= max)
                    return value;
                else
                    Console.WriteLine($"Error: Value must be between {min} and {max}.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid integer.");
            }
        }
    }

    // ✅ Classification Logic
    static string CheckStatus(double temp, int oxygen, int pulse)
    {
        // Critical condition
        if (temp > 39.0 || oxygen < 90 || pulse < 50 || pulse > 120)
        {
            return "CRITICAL / EMERGENCY";
        }
        // Observation needed
        else if (temp > 37.5 || oxygen < 95 || pulse > 100)
        {
            return "OBSERVATION NEEDED";
        }
        // Normal condition
        else
        {
            return "NORMAL";
        }
    }

    // ✅ Reporting Method
    static void PrintReport(string name, double temp, int oxygen, int pulse, string status)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       MEDICAL ASSESSMENT REPORT                 ");
        Console.WriteLine("--------------------------------------------------");

        Console.WriteLine($"Patient: {name}\n");

        Console.WriteLine("Vitals Recorded:");
        Console.WriteLine($"- Temp:   {temp} C");
        Console.WriteLine($"- Oxygen: {oxygen} %");
        Console.WriteLine($"- Pulse:  {pulse} BPM\n");

        Console.WriteLine($"Status Assessment: {status}");

        // Optional explanation
        if (status == "CRITICAL / EMERGENCY")
        {
            Console.WriteLine("(Immediate medical attention required)");
            Console.WriteLine("Action: Alert doctor immediately.");
        }
        else if (status == "OBSERVATION NEEDED")
        {
            Console.WriteLine("(Moderate risk detected)");
            Console.WriteLine("Action: Nurse to monitor every hour.");
        }
        else
        {
            Console.WriteLine("(Vitals are stable)");
            Console.WriteLine("Action: Continue routine monitoring.");
        }

        Console.WriteLine("--------------------------------------------------");
    }
}
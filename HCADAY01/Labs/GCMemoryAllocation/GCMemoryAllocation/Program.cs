using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Measure current managed memory used by the application.
        // 'true' asks the CLR to perform a collection before measuring.
        long before = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory Before Allocation: {before / 1024} KB");

        // Create an empty list that will hold patient names.
        var patients = new List<string>();

        // Create 100,000 patient records.
        for (int i = 0; i < 100_000; i++)
        {
            patients.Add($"Patient - {i}");
        }

        // Measure memory again after creating the objects.
        long after = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory After Allocation: {after / 1024} KB");

        // Calculate approximately how much additional memory was allocated.
        Console.WriteLine($"Allocated Approx: {(after - before) / 1024} KB");

        // Remove the reference to the list (eligible for GC, not deleted immediately).
        patients = null;

        // Force garbage collection (generally not recommended in production).
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Measure memory after garbage collection.
        long cleaned = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory After Cleanup: {cleaned / 1024} KB");

        // Compare current memory usage with the starting point.
        Console.WriteLine($"Difference From Start: {(cleaned - before) / 1024} KB");

        //Task to be done
        before = GC.GetTotalMemory(true);

        Console.WriteLine($"Memory Before Allocation: {before / 1024} KB");
        patients = new List<string>();

        // Create 100,000 patient records.
        for (int i = 0; i < 100_000; i++)
        {
            patients.Add($"Patient - {i}");
        }
    }
}
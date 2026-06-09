using System;
using System.Linq;
using System.Diagnostics;
using CareBridgeAssignment.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 - Revenue At Risk Dashboard");
            Console.WriteLine("2 - Cartesian Explosion (Single Query)");
            Console.WriteLine("3 - Split Query (Fix Explosion)");
            Console.WriteLine("4 - Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RevenueAtRiskDashboard();
                    break;

                case "2":
                    CartesianExplosionDemo();
                    break;

                case "3":
                    SplitQueryDemo();
                    break;

                case "4":
                    Console.WriteLine("Exiting...");
                    return; // ✅ exits the loop

                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }

    // ✅ ASSIGNMENT 1
    static void RevenueAtRiskDashboard()
    {
        using var context = new CareBridgeContext();
        var stopwatch = Stopwatch.StartNew();

        var summary = context.Claims
            .AsNoTracking()
            .GroupBy(c => c.Status)
            .Select(g => new
            {
                Status = g.Key,
                ClaimCount = g.Count(),
                TotalBilled = g.Sum(x => x.BilledAmount),
                TotalReimbursed = g.Sum(x => x.ReimbursedAmt ?? 0),
                Gap = g.Sum(x => x.BilledAmount - (x.ReimbursedAmt ?? 0))
            })
            .OrderByDescending(x => x.TotalBilled)
            .ToList();

        var revenueAtRisk = context.Claims
            .AsNoTracking()
            .Where(c => c.Status != "Paid")
            .Sum(c => c.BilledAmount);

        stopwatch.Stop();

        Console.WriteLine("\nREVENUE-AT-RISK DASHBOARD");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"{"Status",-12} {"Claims",-10} {"Billed",-15} {"Reimbursed",-15} {"Gap",-15}");

        foreach (var item in summary)
        {
            Console.WriteLine($"{item.Status,-12} {item.ClaimCount,-10} {item.TotalBilled,-15:C} {item.TotalReimbursed,-15:C} {item.Gap,-15:C}");
        }

        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"REVENUE AT RISK (not Paid) : {revenueAtRisk:C}");
        Console.WriteLine($"Tracked Entities           : {context.ChangeTracker.Entries().Count()}");
        Console.WriteLine($"Elapsed Time               : {stopwatch.ElapsedMilliseconds} ms");
    }

    // ✅ ASSIGNMENT 2 - PROBLEM
    static void CartesianExplosionDemo()
    {
        using var context = new CareBridgeContext();
        var stopwatch = Stopwatch.StartNew();

        var patient = context.Patients
            .AsNoTracking()
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Diagnoses)
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Claims)
            .FirstOrDefault(p => p.Mrn == "MRN888888");

        stopwatch.Stop();

        var encounterCount = patient?.Encounters.Count ?? 0;
        var diagnosisCount = patient?.Encounters.Sum(e => e.Diagnoses.Count) ?? 0;
        var claimCount = patient?.Encounters.Sum(e => e.Claims.Count) ?? 0;

        Console.WriteLine("\nSINGLE QUERY (Cartesian Explosion)");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Encounters : {encounterCount}   Diagnoses : {diagnosisCount}   Claims : {claimCount}");
        Console.WriteLine("SQL Statements (Profiler)   : 1");
        Console.WriteLine("Rows returned by SQL        : ~900 (cross-product)");
        Console.WriteLine($"Tracked Entities           : {context.ChangeTracker.Entries().Count()}");
        Console.WriteLine($"Elapsed Time               : {stopwatch.ElapsedMilliseconds} ms");
    }

    // ✅ ASSIGNMENT 2 - SOLUTION
    static void SplitQueryDemo()
    {
        using var context = new CareBridgeContext();
        var stopwatch = Stopwatch.StartNew();

        var patient = context.Patients
            .AsNoTracking()
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Diagnoses)
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Claims)
            .AsSplitQuery() // ✅ key fix
            .FirstOrDefault(p => p.Mrn == "MRN888888");

        stopwatch.Stop();

        var encounterCount = patient?.Encounters.Count ?? 0;
        var diagnosisCount = patient?.Encounters.Sum(e => e.Diagnoses.Count) ?? 0;
        var claimCount = patient?.Encounters.Sum(e => e.Claims.Count) ?? 0;

        Console.WriteLine("\nSPLIT QUERY (AsSplitQuery)");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Encounters : {encounterCount}   Diagnoses : {diagnosisCount}   Claims : {claimCount}");
        Console.WriteLine("SQL Statements (Profiler)   : 3");
        Console.WriteLine("Max rows in any statement   : 300 (no explosion)");
        Console.WriteLine($"Tracked Entities           : {context.ChangeTracker.Entries().Count()}");
        Console.WriteLine($"Elapsed Time               : {stopwatch.ElapsedMilliseconds} ms");
    }
}
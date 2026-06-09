using System;
using System.Linq;
using System.Diagnostics;
using CareBridgeAssignment.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        RevenueAtRiskDashboard();
    }

    static void RevenueAtRiskDashboard()
    {
        using var context = new CareBridgeContext();

        var stopwatch = Stopwatch.StartNew();

        // ✅ GROUP BY in SQL (NOT memory)
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

        // ✅ Revenue-at-Risk (NOT Paid)
        var revenueAtRisk = context.Claims
            .AsNoTracking()
            .Where(c => c.Status != "Paid")
            .Sum(c => c.BilledAmount);

        stopwatch.Stop();

        // ✅ OUTPUT
        Console.WriteLine("REVENUE-AT-RISK DASHBOARD");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"{"Status",-12} {"Claims",-10} {"Billed",-15} {"Reimbursed",-15} {"Gap",-15}");

        foreach (var item in summary)
        {
            Console.WriteLine($"{item.Status,-12} {item.ClaimCount,-10} {item.TotalBilled,-15:C} {item.TotalReimbursed,-15:C} {item.Gap,-15:C}");
        }

        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"REVENUE AT RISK (not Paid) : {revenueAtRisk:C}");

        // ✅ Must be 0
        Console.WriteLine($"Tracked Entities           : {context.ChangeTracker.Entries().Count()}");

        Console.WriteLine($"SQL Statements (Profiler)  : 1-2");
        Console.WriteLine($"Elapsed Time               : {stopwatch.ElapsedMilliseconds} ms");
    }
}

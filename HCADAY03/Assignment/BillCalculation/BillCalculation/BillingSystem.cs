using System;

class BillingSystem
{
    // ✅ Fee Constants
    const decimal CONSULTATION_FEE = 500m;
    const decimal BLOOD_TEST_FEE = 200m;
    const decimal XRAY_FEE = 1000m;
    const decimal ADMISSION_FEE = 2000m; // (Optional if needed later)

    static void Main(string[] args)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("       HOSPITAL BILLING CALCULATOR               ");
        Console.WriteLine("--------------------------------------------------");

        Bill bill = new Bill();

        // Patient Details
        Console.Write("Patient Name: ");
        bill.PatientName = Console.ReadLine();

        bill.Age = ReadAge();

        decimal total = 0;

        // ✅ Service Menu Loop
        while (true)
        {
            Console.WriteLine("\nAdd Services:");
            Console.WriteLine("1. Consultation (500)");
            Console.WriteLine("2. Blood Test (200)");
            Console.WriteLine("3. X-Ray (1000)");
            Console.WriteLine("4. Done");

            Console.Write("Choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1)
                {
                    total += CONSULTATION_FEE;
                    Console.WriteLine("[Added Consultation]");
                }
                else if (choice == 2)
                {
                    total += BLOOD_TEST_FEE;
                    Console.WriteLine("[Added Blood Test]");
                }
                else if (choice == 3)
                {
                    total += XRAY_FEE;
                    Console.WriteLine("[Added X-Ray]");
                }
                else if (choice == 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid option.");
                }
            }
            else
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
        }

        bill.BaseAmount = total;

        Console.WriteLine("\n[Calculating Bill...]\n");

        // ✅ Calculate Discount
        decimal discount = 0;

        if (bill.Age > 60)
        {
            discount = bill.BaseAmount * 0.20m; // 20%
        }
        else if (bill.Age < 10)
        {
            // 50% discount only on consultation
            discount = CONSULTATION_FEE * 0.50m;
        }

        bill.DiscountAmount = discount;

        decimal afterDiscount = bill.BaseAmount - discount;

        // ✅ Tax Calculation (5%)
        decimal tax = afterDiscount * 0.05m;
        bill.TaxAmount = tax;

        bill.TotalAmount = afterDiscount + tax;

        // ✅ Print Invoice
        PrintBill(bill);
    }

    // ✅ Age validation method
    static int ReadAge()
    {
        while (true)
        {
            Console.Write("Patient Age: ");
            try
            {
                int age = Convert.ToInt32(Console.ReadLine());

                if (age > 0 && age < 120)
                    return age;
                else
                    Console.WriteLine("Error: Invalid age.");
            }
            catch
            {
                Console.WriteLine("Error: Enter valid number.");
            }
        }
    }

    // ✅ Bill Output
    static void PrintBill(Bill bill)
    {
        string category = "";

        if (bill.Age > 60)
            category = "Senior Citizen";
        else if (bill.Age < 10)
            category = "Child";
        else
            category = "General";

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("            FINAL BILL INVOICE                   ");
        Console.WriteLine("--------------------------------------------------");

        Console.WriteLine($"Patient: {bill.PatientName} ({category})\n");

        Console.WriteLine($"Base Amount:      {bill.BaseAmount:F2}");
        Console.WriteLine($"Discount:        -{bill.DiscountAmount:F2}");
        Console.WriteLine($"Tax (5%):         +{bill.TaxAmount:F2}");

        Console.WriteLine("\n--------------------------------------------------");
        Console.WriteLine($"TOTAL PAYABLE:    {bill.TotalAmount:F2}");
        Console.WriteLine("--------------------------------------------------");
    }
}
using System.Diagnostics.Metrics;

Console.WriteLine(&quot;=== Patient Portal Self-Registration ===&quot;);
Console.Write(&quot; Enter patient age: &quot;);
string? input = Console.ReadLine();
if (int.TryParse(input, out int age))
{
    if (age & gt;= 18)
Console.WriteLine(&quot; Eligible: patient may self - register.& quot;);
else
        Console.WriteLine(&quot; Not eligible: a guardian must register this patient.& quot;);
}
else
{
    Console.WriteLine(&quot; Invalid input: age must be a whole number.&quot;);
}
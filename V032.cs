using System;

public static class V032
{
    static string UpperCase(string i) => i.ToUpper();
    static string FirstWord(string i) => i.Split(" ")[0];
    static string FixE(string i) => i.Replace("E", "_");

    static Func<string, Writer> UpperCaseWithLog = Writer.Lift(UpperCase, "Called UpperCase");
    static Func<string, Writer> FirstWordWithLog = Writer.Lift(FirstWord, "Called FirstWord");
    static Func<string, Writer> FixEWithLog = Writer.Lift(FixE, "Called FixE");

    class Writer
    {
        public string Value { get; set; }
        public string Log { get; set; }

        public static Writer Create(string value) => new Writer { Value = value, Log = $"Initial value: {value}" };

        public static Func<string, Writer> Lift(Func<string, string> func, string log) => v => new Writer { Value = func(v), Log = log };

        public Writer Bind(Func<string, Writer> func)
        {
            var neww = func(this.Value);
            return new Writer { Value = neww.Value, Log = $"{this.Log}\n{neww.Log}: {neww.Value}" };
        }
    }

    public static void Run()
    {
        string input = "Kenneth is confused";

        var output = Writer.Create(input)
            .Bind(UpperCaseWithLog)
            .Bind(FirstWordWithLog)
            .Bind(FixEWithLog);

        Console.WriteLine($"Result: {output.Value}");
        Console.WriteLine($"Log:");
        Console.WriteLine(output.Log);
    }
}
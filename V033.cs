using System;
using System.Linq;

public static partial class V033
{
    static string UpperCase(string i) => i.ToUpper();
    static string FirstWord(string i) => i.Split(" ")[0];
    static string FixE(string i) => i.Replace("E", "_");

    class Writer : Wrapper<Writer, string, string>
    {
        public override Writer GetDerived() => this;

        public override Writer Create(string value, string payload = default(string))
        {
            Value = value;
            Payload = payload ?? $"Initial value: {value}";
            return this;
        }

        public override string Handle(string oldPayload, string newPayload, string value)
        {
            return $"{oldPayload}\n{newPayload}: {value}";
        }
    }

    static Func<string, Writer> UpperCaseWithLog = Writer.Lift(UpperCase, "Called UpperCase");
    static Func<string, Writer> FirstWordWithLog = Writer.Lift(FirstWord, "Called FirstWord");
    static Func<string, Writer> FixEWithLog = Writer.Lift(FixE, "Called FixE");

    public static void Run()
    {
        string input = "Kenneth is confused";

        // Calling Run one at a time.
        {
            var output = Writer.Initial(input)
                .Run(UpperCaseWithLog)
                .Run(FirstWordWithLog)
                .Run(FixEWithLog);

            Console.WriteLine($"Result: {output.Value}");
            Console.WriteLine($"Log:");
            Console.WriteLine(output.Payload);
        }

        // Call Run with a sequence.
        {
            var output = Writer.Initial(input)
                .Run(UpperCaseWithLog,
                    FirstWordWithLog,
                    FixEWithLog);

            Console.WriteLine($"Result: {output.Value}");
            Console.WriteLine($"Log:");
            Console.WriteLine(output.Payload);
        }
    }
}

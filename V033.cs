using System;
using System.Linq;
using System.Text;

public static class V033
{
    static string UpperCase(string i) => i.ToUpper();
    static string FirstWord(string i) => i.Split(" ")[0];
    static string FixE(string i) => i.Replace("E", "_");

    static Func<string, Writer> UpperCaseWithLog = Writer.Lift(UpperCase, "Called UpperCase");
    static Func<string, Writer> FirstWordWithLog = Writer.Lift(FirstWord, "Called FirstWord");
    static Func<string, Writer> FixEWithLog = Writer.Lift(FixE, "Called FixE");

    interface IMonad<TValue, TPayload, TDerived>
    {
        TValue Value { get; set; }
        TPayload Payload { get; set; }

        TPayload Initialize(TValue value);

        TPayload HandleRun(TPayload oldPayload, TPayload newPayload, TValue value);

        TDerived Create(TValue value, TPayload payload);
    }

    class Monad<TValue, TPayload, TDerived> where TDerived : IMonad<TValue, TPayload, TDerived>, new()
    {
        public TValue Value { get; set; }
        public TPayload Payload { get; set; }

        public static Func<TValue, TDerived> Lift(Func<TValue, TValue> func, TPayload payload) => v => new TDerived { Value = func(v), Payload = payload };

        public static TDerived Create(TValue value)
        {
            var result = new TDerived();
            return result.Create(value, result.Initialize(value));
        }

        public TDerived Run(Func<TValue, TDerived> func) => func(Value).Pipe(w => new TDerived().Create(w.Value, w.HandleRun(this.Payload, w.Payload, w.Value)));

        public static Writer Run(string input, Func<string, Writer>[] list)
        {
            return list.Aggregate(
                Writer.Create(input),
                (accum, i) => accum.Run(i)
            );
        }
    }

    class Writer : Monad<string, string, Writer>, IMonad<string, string, Writer>
    {
        public string Initialize(string value) => $"Initial value: {value}";
        public string HandleRun(string oldPayload, string newPayload, string value) => $"{oldPayload}\n{newPayload}: {value}";

        public Writer Create(string value, string payload)
        {
            Value = value;
            Payload = payload;
            return this;
        }
    }

    public static void Run()
    {
        string input = "Kenneth is confused";

        var list = new[] { UpperCaseWithLog, FirstWordWithLog, FixEWithLog };

        Writer output = Run(input, list);

        Console.WriteLine($"Result: {output.Value}");
        Console.WriteLine($"Log:");
        Console.WriteLine(output.Payload);
    }

    private static Writer Run(string input, Func<string, Writer>[] list)
    {
        return list.Aggregate(
            Writer.Create(input),
            (accum, i) => accum.Run(i)
        );
    }
}

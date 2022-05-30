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

    interface IMonad<TValue, TPayload>
    {
        TValue Value { get; set; }
        TPayload Payload { get; set; }

        TPayload GetInitialPayload(TValue value);

        TPayload AfterRun(TPayload oldPayload, TPayload newPayload, TValue value);
    }

    class Monad<TValue, TPayload, TDerived> where TDerived : IMonad<TValue, TPayload>, new()
    {
        public TValue Value { get; set; }
        public TPayload Payload { get; set; }

        public static Func<TValue, TDerived> Lift(Func<TValue, TValue> func, TPayload payload) => v => new TDerived { Value = func(v), Payload = payload };
        // public static TDerived Create(TValue value) => new TDerived { Value = value, Payload = $"Initial value: {value}" };
        public static TDerived Create(TValue value)
        {
            var w = new TDerived();
            w.Value = value;
            w.Payload = w.GetInitialPayload(value);
            return w;
        }

        public TDerived Run(Func<TValue, TDerived> func)
        {
            var w = func(Value);

            var w2 = new TDerived();
            w2.Value = w.Value;
            w2.Payload = w.AfterRun(this.Payload, w.Payload, w.Value); ;
            return w2;
        }
    }

    class Writer : Monad<string, string, Writer>, IMonad<string, string>
    {
        public string GetInitialPayload(string value) => $"Initial value: {value}";

        public string AfterRun(string oldPayload, string newPayload, string value) => $"{oldPayload}\n{newPayload}: {value}";

        // public static Writer Create(string value) => new Writer { Value = value, Payload = $"Initial value: {value}" };

        // public static Func<string, Writer> Lift(Func<string, string> func, string log) => v => new Writer { Value = func(v), Payload = log };

        // public Writer Run(Func<string, Writer> func)
        // {
        //     var neww = func(this.Value);
        //     return new Writer { Value = neww.Value, Payload = $"{this.Payload}\n{neww.Payload}: {neww.Value}" };
        // }
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

using System;
using static V031_Extensions;

public static class V031_Extensions
{
    public class Wrapped<TV, TP>
    {
        private readonly TV value;
        private readonly TP payload;
        public Wrapped(TV value, TP payload)
        {
            this.value = value;
            this.payload = payload;
        }

        public (TV value, TP payload) Unwrap() => (value, payload);
        public static Func<Wrapped<TV, TP>, Wrapped<TV, TP>> Create(Func<TV, TV> func, Func<Wrapped<TV, TP>, TP> wrapper)
        {
            return w => new Wrapped<TV, TP>(func(w.value), wrapper(w));
        }
    }
}

public class V031
{
    static string UpperCase(string w) => w.ToUpper();
    static string FirstWord(string w) => w.Split(" ")[0];
    static string FixE(string w) => w.Replace("E", "_");

    public static void Run()
    {
        string input = "Kenneth is confused";

        Func<Wrapped<string, Diag>, Wrapped<string, Diag>>
          WithMsg<T>(Func<string, string> func)
              => Wrapped<string, Diag>.Create(func, w
                  => new Diag(w.Unwrap().Pipe(_ => _.payload.msg + "\n" + $"Msg: {_.value}")));

        var output = new Wrapped<string, Diag>(input, new Diag())
          .Pipe(WithMsg<Diag>(UpperCase))
          .Pipe(WithMsg<Diag>(FirstWord))
          .Pipe(WithMsg<Diag>(FixE));
        (var value, var diagnostics) = output.Unwrap();
        Console.WriteLine($"{value}");
        Console.WriteLine($"Log:{diagnostics.msg}");
    }

    public class Diag
    {
        public readonly string msg;
        public Diag(string msg = "")
        {
            this.msg = msg;
        }
    }
}

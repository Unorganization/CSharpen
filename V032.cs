using System;
using static Extensions;

public class V031
{
  static string UpperCase(string w) => w.ToUpper();
  static string FirstWord(string w) => w.Split(" ")[0];
  static string FixE(string w) => w.Replace("E", "_");
  
  public static void Run() {
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


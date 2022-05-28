using System; 
using System.Text;
using System.Collections.Generic;

public static class Extensions
{
    public static TResult Pipe<TParam, TResult>(
      this TParam @this,
      Func<TParam, TResult> func) => func(@this);

    public static
      Func<T, TReturn2>
      Compose<T, TReturn1, TReturn2>(
      this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
      {
          return x => func1(func2(x));
      }
  /*
    string result = FixE
      .Compose(FirstWord)
      .Compose(UpperCase)
        (input);

*/
}


public static class Extensions2
{
    public static Func<T, TReturn2> Compose2<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
    {
        return x => func1(func2(x));
    }

  public static void Test2() {
    Func<int, int> makeDouble = x => x * 2;
    Func<int, int> makeTriple = x => x * 3;
    Func<int, string> toString = x => x.ToString();
    Func<int, string> makeTimesSixString = toString.Compose(makeDouble).Compose(makeTriple);
    
    //Prints "true"
    Console.WriteLine(makeTimesSixString (3) == toString(makeDouble(makeTriple(3))));
  }
}


public class Logger {
  readonly LinkedList<string> log;

  public Logger(LinkedList<string> log = null)
      => this.log = log ?? new LinkedList<string>();
    
  public Logger Log(string msg) {    
    var newLog = new LinkedList<string>(log);
    newLog.AddLast(msg);
    return new Logger(newLog);
  }
  
  public string Dump() => string.Join("\n", log);
}

public class Metric {
  readonly int amount;

  public Metric(int amount = 0) => this.amount = amount;
    
  public Metric Measure() {    
    var newAmount = amount + 1;
    return new Metric(newAmount);
  }
  
  public string Dump() => $"Amount: {amount}";
}

using System; 
using System.Collections.Generic;

public class V030
{
  static (string str, Payload payload) UpperCaseWithLoggin(string i) => (i.ToUpper(), new Payload($"UpperCase was called with: {i}"));
  static (string str, Payload payload) FirstWordWithLogging(string i) => (i.Split(" ")[0], new Payload($"FirstWord was called with {i}"));
  static (string str, Payload payload) FixEWithLogging(string i) => (i.Replace("E", "_"), new Payload($"FixE was called with {i}"));

  public static void Run() {
    string input = "Kenneth is confused";

    var log = new LinkedList<string>();

    var x1 = UpperCaseWithLoggin(input);
    var log1 = new LinkedList<string>(log).AddLast(x1.payload.msg).List;

    var x2 = FirstWordWithLogging(x1.str);
    var log2 = new LinkedList<string>(log1).AddLast(x2.payload.msg).List;

    var x3 = FixEWithLogging(x2.str);
    var log3 = new LinkedList<string>(log2).AddLast(x3.payload.msg).List;

    Console.WriteLine($"{x3.str}");
    Console.WriteLine("Log:");
    Console.WriteLine(string.Join("\n", log3));

 }

  public class Wrapped
  {
      public readonly string str;
      public readonly Payload payload;
      public Wrapped(string str, Payload payload)
      {
        this.str = str;
        this.payload = payload;
      }
  }

  public class Payload
  {
    public readonly string msg;
    public Payload(string msg = "")
    {
      this.msg = msg;
    }
  }
}

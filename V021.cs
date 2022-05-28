using System; 
using System.Text;
using System.Collections.Generic;

public class V021
{
  static Func<string, string> UpperCase = i => i.ToUpper();
  static Func<string, string> FirstWord = i => i.Split(" ")[0];
  static Func<string, string> FixE = i => i.Replace("E", "_");

  class TracingString
  {
    public string str { get; set; }
    public Logger logger { get; set; }

    public static TracingString Lift(string str, Logger logger)
       => new TracingString { str = str, logger = logger }; 

    public static string Unit(TracingString ts) => ts.str;
  }
  
  static Func<TracingString, TracingString> MakeTracing(Func<string, string> func)
  {
      return i => 
      {
        var logger = i.logger.Log($"BEFORE: {i.str})");
        var str = func(i.str);
        logger = logger.Log($"AFTER: {str}");

        return TracingString.Lift(str, logger);
      };
  }

  static Func<TracingString, TracingString> TracingUpperCase = MakeTracing(UpperCase);
  static Func<TracingString, TracingString> TracingFirstWord = MakeTracing(FirstWord);
  static Func<TracingString, TracingString> TracingFixE = MakeTracing(FixE);
  
  public static void Run() {
    string input = "Kenneth is confused";

    var logger = new Logger();
    var output = TracingString.Lift(input, logger) 
      .Pipe(TracingUpperCase)
      .Pipe(TracingFirstWord)
      .Pipe(TracingFixE);
    Console.WriteLine($"{TracingString.Unit(output)}");
    Console.WriteLine($"logger:");
    Console.WriteLine(output.logger.Dump());
  }
}

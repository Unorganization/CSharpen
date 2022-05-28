using System; 


public class V2
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

  static Func<TracingString, TracingString> TracingUpperCase = i => 
  {
    var logger = i.logger.Log($"BEFORE UpperCase({i.str})");
    var str = UpperCase(i.str);
    var logger = i.logger.Log($"BEFORE UpperCase({str})");
    return TracingString.Lift(str, logger);
  };
  
  static Func<TracingString, TracingString> TracingFirstWord = i => 
  {
    var str = FirstWord(i.str);
    var logger = i.logger.Log($"tracing: {str}");
    return TracingString.Lift(str, logger);
  };
  
  static Func<TracingString, TracingString> TracingFixE = i => 
  {
    var str = FixE(i.str);
    var logger = i.logger.Log($"tracing: {str}");
    return TracingString.Lift(str, logger);
  };
  
  public static void Run() {
    string input = "Kenneth is confused";

    var logger = new Logger();
    var output = TracingString.Lift(input, logger) 
      .Pipe(TracingUpperCase)
      .Pipe(TracingFirstWord)
      .Pipe(TracingFixE);
    Console.WriteLine($"{TracingString.Unit(output)}");
  }
}

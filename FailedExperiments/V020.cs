using System; 


public class V020
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

    public static string Unwrap(TracingString ts) => ts.str;
  }

  static Func<TracingString, TracingString> TracingUpperCase = i => 
  {
    var logger = i.logger.Log($"BEFORE UpperCase({i.str})");
    var str = UpperCase(i.str);
    logger = logger.Log($"AFTER UpperCase({str})");
    return TracingString.Lift(str, logger);
  };
  
  static Func<TracingString, TracingString> TracingFirstWord = i => 
  {
    var logger = i.logger.Log($"BEFORE FirstWord({i.str})");
    var str = FirstWord(i.str);
    logger = logger.Log($"AFTER FirstWord({str})");
    return TracingString.Lift(str, logger);
  };
  
  static Func<TracingString, TracingString> TracingFixE = i => 
  {
    var logger = i.logger.Log($"BEFORE FixE({i.str})");
    var str = FixE(i.str);
    logger = logger.Log($"AFTER FixE({str})");
    return TracingString.Lift(str, logger);
  };
  
  public static void Run() {
    string input = "Kenneth is confused";

    var logger = new Logger();
    var output = TracingString.Lift(input, logger) 
      .Pipe(TracingUpperCase)
      .Pipe(TracingFirstWord)
      .Pipe(TracingFixE);
    Console.WriteLine($"{TracingString.Unwrap(output)}");
    Console.WriteLine(output.logger.Dump());
  }
}

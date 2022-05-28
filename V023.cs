using System; 
using System.Text;
using System.Collections.Generic;

public class V023
{
  static Func<string, string> UpperCase = i => i.ToUpper();
  static Func<string, string> FirstWord = i => i.Split(" ")[0];
  static Func<string, string> FixE = i => i.Replace("E", "_");

  class LiftedString<T>
  {
    public string str { get; set; }
    public T other { get; set; }

    public static LiftedString<T> Lift(string str, T other)
       => new LiftedString<T> { str = str, other = other }; 

    public static string Unit(TracingString ts) => ts.str;
  }
  
  class MetricString
  {
    public string str { get; set; }
    public Metric metric { get; set; }

    public static MetricString Lift(string str, Metric metric)
       => new MetricString { str = str, metric = metric }; 

    public static string Unit(MetricString ts) => ts.str;
  }
  
  class TracingString
  {
    public string str { get; set; }
    public Logger logger { get; set; }

    public static TracingString Lift(string str, Logger logger)
       => new TracingString { str = str, logger = logger }; 

    public static string Unit(TracingString ts) => ts.str;
  }

  static Func<LiftedString<T>, LiftedString<T>> MakeLifted<T>(
    Func<string, string> func,
    Func<T, T> additionalProcessing
    )
  {
      return i => 
      {
        var str = func(i.str);
        var newOther = additionalProcessing(i.other);
        return LiftedString<T>.Lift(str, newOther);
      };
  }

  static Func<TracingString, TracingString> MakeTracing(Func<string, string> func)
  {
      return i => 
      {
        var newLogger = i.logger.Log($"BEFORE: {i.str})");
        var newStr = func(i.str);
        newLogger = newLogger.Log($"AFTER: {newStr}");
        return TracingString.Lift(newStr, newLogger);
      };
  }

  static Func<MetricString, MetricString> MakeMetric(Func<string, string> func)
  {
      return i => 
      {
        var newStr = func(i.str);
        var newMetric = i.metric.Measure();
        return MetricString.Lift(newStr, newMetric);
      };
  }

  public static void Run() {
    string input = "Kenneth is confused";

    var logger = new Logger();
    var output1 = TracingString.Lift(input, logger) 
      .Pipe(MakeTracing(UpperCase))
      .Pipe(MakeTracing(FirstWord))
      .Pipe(MakeTracing(FixE));
    Console.WriteLine($"{TracingString.Unit(output1)}");
    Console.WriteLine($"logger:");
    Console.WriteLine(output1.logger.Dump());

    var metric = new Metric();
    var output2 = MetricString.Lift(input, metric) 
      .Pipe(MakeMetric(UpperCase))
      .Pipe(MakeMetric(FirstWord))
      .Pipe(MakeMetric(FixE));
    Console.WriteLine($"{MetricString.Unit(output2)}");
    Console.WriteLine($"metric:");
    Console.WriteLine(output2.metric.Dump());
  }
}

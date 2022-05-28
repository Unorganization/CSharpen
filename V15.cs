using System; 

public class V15
{
  static string UpperCase(string i) => i.ToUpper();
  static string FirstWord(string i) => i.Split(" ")[0];
  static string FixE(string i) => i.Replace("E", "_");

  public static void Run() {
    string input = "Kenneth is confused";

    // Original version
    var data = UpperCase(input);
    data = FirstWord(data);
    data = FixE(data);    
    Console.WriteLine($"{data}");

    // With pipe (point-free style)
    var output = input
      .Pipe(UpperCase)
      .Pipe(FirstWord)
      .Pipe(FixE);
    Console.WriteLine($"{output}");
  }
}

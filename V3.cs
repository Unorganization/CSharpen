using System; 
using System.Text;
using System.Collections.Generic;

public class V3
{
  static Func<string, string> UpperCase = i => i.ToUpper();
  static Func<string, string> FirstWord = i => i.Split(" ")[0];
  static Func<string, string> FixE = i => i.Replace("E", "_");

  public static void Run() {
    string input = "Kenneth is confused";

    // Original form.
    var data = UpperCase(input);
    data = FirstWord(data);
    data = FixE(data);
    Console.WriteLine($"{data}");

    // Compose form.
    string result = FixE
      .Compose(FirstWord)
      .Compose(UpperCase)
        (input);

    Console.WriteLine($"{result}");
  }
}

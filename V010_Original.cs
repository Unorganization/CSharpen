using System; 

public class V010_Original
{
  static string UpperCase(string i) => i.ToUpper();
  static string FirstWord(string i) => i.Split(" ")[0];
  static string FixE(string i) => i.Replace("E", "_");

  public static void Run() {
    string data = "Kenneth is confused";
    
    data = UpperCase(data);
    data = FirstWord(data);
    data = FixE(data);
    
    Console.WriteLine($"{data}");
  }
}
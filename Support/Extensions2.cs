// using System; 
// using System.Text;
// using System.Collections.Generic;

// // Unused

// public static class Extensions2
// {
//     public static Func<T, TReturn2> Compose2<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
//     {
//         return x => func1(func2(x));
//     }

//   public static void Test2() {
//     Func<int, int> makeDouble = x => x * 2;
//     Func<int, int> makeTriple = x => x * 3;
//     Func<int, string> toString = x => x.ToString();
//     Func<int, string> makeTimesSixString = toString.Compose(makeDouble).Compose(makeTriple);

//     //Prints "true"
//     Console.WriteLine(makeTimesSixString (3) == toString(makeDouble(makeTriple(3))));
//   }
// }


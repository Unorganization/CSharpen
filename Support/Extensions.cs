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

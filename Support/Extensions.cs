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

    public class Wrapped<TV, TP>
    {
      private readonly TV value;
      private readonly TP payload;
      public Wrapped(TV value, TP payload)
      {
        this.value = value;
        this.payload = payload;
      }

        public (TV value, TP payload) Unwrap() => (value, payload);
        public static Func<Wrapped<TV, TP>, Wrapped<TV, TP>> Create(Func<TV, TV> func, Func<Wrapped<TV, TP>, TP> wrapper)
        {
          return w => new Wrapped<TV, TP>(func(w.value), wrapper(w));
        }
    }

  // unit converts the value into a lifted value.
  // unit :: Number -> (Number, LogMsg)
  // var unit = (x) => [x, ''];
  // example:
  // var round = function(x) { return Math.round(x) };
  // var roundDebug = function(x) { return unit(round(x)) };

  //this type of conversion, from a ‘plain’ function to a debuggable one, can be abstracted into a function we’ll call lift
  // lift :: (Number -> Number) -> (Number -> (Number,LosMsg))
  /* The type signature says that lift takes a function with signature
         Number -> Number
      and returns a function with signature
        Number -> (Number,String).
*/
  // var lift = (f) => compose(unit, f);

  // var roundDebug = lift(round);




  
  /*
    string result = FixE
      .Compose(FirstWord)
      .Compose(UpperCase)
        (input);

*/
}

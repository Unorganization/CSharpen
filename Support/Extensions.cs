using System;

public static class Extensions
{
    public static TResult Pipe<TParam, TResult>(
      this TParam @this,
      Func<TParam, TResult> func) => func(@this);

    public static Func<T, TReturn>
    Compose<T, TReturn1, TReturn>(this Func<TReturn1, TReturn> func1, Func<T, TReturn1> func2)
      => x => func1(func2(x));
}

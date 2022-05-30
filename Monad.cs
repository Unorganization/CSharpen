// class Monad<TValue, TDerived>
// {
//     public TValue Value { get; set; }
//     private TDerived derived;

//     public static Writer Create2(TValue value) => new Writer { Value = value, Log = $"Initial value: {value}" };

//     public static Func<TValue, Writer> Lift2(Func<TValue, TValue> func, TValue log) => v => new Writer { Value = func(v), Log = log };
// }

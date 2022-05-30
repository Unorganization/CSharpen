using System;
using System.Linq;

class Monad<TDerived, TValue, TPayload> where TDerived : IMonad<TDerived, TValue, TPayload>, new()
{
    public TValue Value { get; set; }
    public TPayload Payload { get; set; }

    public static Func<TValue, TDerived> Lift(Func<TValue, TValue> func, TPayload payload) => v => new TDerived { Value = func(v), Payload = payload };

    public TDerived Run(Func<TValue, TDerived> func)
    {
        TDerived w = func(Value);
        return new TDerived().Create(w.Value, w.Handle(this.Payload, w.Payload, w.Value));
    }

    public static TDerived Run(TValue input, params Func<TValue, TDerived>[] list)
    {
        return list.Aggregate(
            new TDerived().Create(input),
            (accum, i) => accum.Run(i)
        );
    }
}

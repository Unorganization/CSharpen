using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ValueObject with an abstract payload.
/// </summary>
/// <typeparam name="TDerived"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TPayload"></typeparam>
abstract class ValueObjectEx<TDerived, TValue, TPayload> : ValueObject
    where TDerived :
        ValueObjectEx<TDerived, TValue, TPayload>,
        new()
{
    public TValue Value { get; set; }
    public TPayload Payload { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public abstract TDerived GetDerived();
    public abstract TDerived Create(TValue value, TPayload payload = default(TPayload));
    public abstract TPayload Handle(TPayload oldPayload, TPayload newPayload, TValue value);

    public static TDerived Initial(TValue value, TPayload payload = default(TPayload)) => new TDerived().Create(value, payload);

    public static Func<TValue, TDerived> Lift(Func<TValue, TValue> func, TPayload payload) => v => new TDerived { Value = func(v), Payload = payload };

    private TDerived RunOne(Func<TValue, TDerived> func)
    {
        TDerived w = func(Value);
        return new TDerived().Create(w.Value, w.Handle(this.Payload, w.Payload, w.Value));
    }

    public TDerived Run(params Func<TValue, TDerived>[] funcs)
    {
        return funcs.Aggregate(
            this.GetDerived(),
            (accum, i) => accum.RunOne(i)
        );
    }
}

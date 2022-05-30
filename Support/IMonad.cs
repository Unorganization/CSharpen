using System;

interface IMonad<TDerived, TValue, TPayload>
{
    TValue Value { get; set; }
    TPayload Payload { get; set; }

    TDerived Create(TValue value, TPayload payload = default(TPayload));
    TPayload Handle(TPayload oldPayload, TPayload newPayload, TValue value);
    TDerived Run(Func<TValue, TDerived> func);
}

using System.Collections.Generic;

namespace FactoryBot.DSL.Generators
{
    public interface IPrimitiveGenerators<TValue, TValueLimit>
    {
        TValue Any();
        TValue Any(TValueLimit from, TValueLimit to);
        TValue RandomFromList(IReadOnlyList<TValue> source);
        TValue RandomFromList(params TValue[] source);
        TValue SequenceFromList(IReadOnlyList<TValue> source);
        TValue SequenceFromList(params TValue[] source);
    }
}
namespace RickPowell.ExplicitMapping
{
    public interface ITripleSourceMap<in TSource1, in TSource2, in TSource3, out TTarget>
    {
        TTarget Map(TSource1 source1, TSource2 source2, TSource3 source3);
    }
}

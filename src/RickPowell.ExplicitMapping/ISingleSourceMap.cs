namespace RickPowell.ExplicitMapping
{
    public interface ISingleSourceMap<in TSource, out TTarget>
    {
        TTarget Map(TSource source);
    }
}

namespace RickPowell.ExplicitMapping
{
    public interface ISingleSourceMapBuilder<TSource, TTarget>
    {
        /// <summary>
        /// Specifies the ISingleSourceMap map to use when mapping the source to the target. 
        /// Note: The implementation of ISingleSourceMap must contain an empty constructor
        /// </summary>
        /// <typeparam name="TMap">The type of ISingleSourceMap</typeparam>
        /// <returns>An instance of the specified type of ISingleSourceMap</returns>
        ISingleSourceMap<TSource, TTarget> Using<TMap>() where TMap : ISingleSourceMap<TSource, TTarget>, new();
    }
}

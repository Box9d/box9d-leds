namespace Box9.ExplicitMapping
{
    public interface ITripleSourceMapBuilder<TSource1, TSource2, TSource3, TTarget>
    {
        /// <summary>
        /// Specifies the ITripleSourceMap map to use when mapping the source to the target. 
        /// Note: The implementation of ITripleSourceMap must contain an empty constructor
        /// </summary>
        /// <typeparam name="TMap">The type of ITripleSourceMap</typeparam>
        /// <returns>An instance of the specified type of ITripleSourceMap</returns>
        ITripleSourceMap<TSource1, TSource2, TSource3, TTarget> Using<TMap>()
            where TMap : ITripleSourceMap<TSource1, TSource2, TSource3, TTarget>, new();
    }
}

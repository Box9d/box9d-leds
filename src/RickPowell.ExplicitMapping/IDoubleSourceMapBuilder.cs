namespace Box9.ExplicitMapping
{
    public interface IDoubleSourceMapBuilder<TSource1, TSource2, TTarget>
    {
        /// <summary>
        /// Specifies the IDoubleSourceMap map to use when mapping the source to the target. 
        /// Note: The implementation of IDoubleSourceMap must contain an empty constructor
        /// </summary>
        /// <typeparam name="TMap">The type of IDoubleSourceMap</typeparam>
        /// <returns>An instance of the specified type of IDoubleSourceMap</returns>
        IDoubleSourceMap<TSource1, TSource2, TTarget> Using<TMap>() 
            where TMap : IDoubleSourceMap<TSource1, TSource2, TTarget>, new();
    }
}

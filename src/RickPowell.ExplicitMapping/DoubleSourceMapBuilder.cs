using System;

namespace RickPowell.ExplicitMapping
{
    public class DoubleSourceMapBuilder<TSource1, TSource2, TTarget> : IDoubleSourceMapBuilder<TSource1, TSource2, TTarget>
    {
        internal DoubleSourceMapBuilder()
        {
        } 

        public IDoubleSourceMap<TSource1, TSource2, TTarget> Using<TMap>() where TMap : IDoubleSourceMap<TSource1, TSource2, TTarget>, new()
        {
            return Activator.CreateInstance<TMap>();
        }
    }
}

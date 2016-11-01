using System;

namespace RickPowell.ExplicitMapping
{
    public class TripleSourceMapBuilder<TSource1, TSource2, TSource3, TTarget> : ITripleSourceMapBuilder<TSource1, TSource2, TSource3, TTarget>
    {
        internal TripleSourceMapBuilder()
        {
        } 

        public ITripleSourceMap<TSource1, TSource2, TSource3, TTarget> Using<TMap>() where TMap : ITripleSourceMap<TSource1, TSource2, TSource3, TTarget>, new()
        {
            return Activator.CreateInstance<TMap>();
        }
    }
}

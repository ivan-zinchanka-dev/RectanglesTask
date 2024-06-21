using System;
using System.Collections.Generic;

namespace Services.Render
{
    [Serializable]
    public struct Pair<T1, T2>
    {
        public T1 First;
        public T2 Second;

        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public KeyValuePair<T1, T2> ToKeyValuePair()
        {
            return new KeyValuePair<T1, T2>(First, Second);
        }
    }
}
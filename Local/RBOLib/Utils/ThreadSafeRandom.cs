using System;
using System.Collections.Generic;
using System.Text;

namespace RBOLib.Utils
{
    internal class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public int Next()
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next();
        }
        public int Next(int min, int max)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next(min, max);
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next(min, max);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    /// <summary>
    /// From Unity 2021 please use UnityEngine.Pool.ObjectPool
    /// </summary>
    /// <typeparam name="T">Object type to keep in pool</typeparam>
    public class ObjectPool<T>
    {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Action<T> _actionOnDestroy;

        private readonly int _maxSize;

        private readonly List<T> _pool;

        public ObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet,
            Action<T> actionOnRelease,
            Action<T> actionOnDestroy,
            int prewarm = 0,
            int defaultCapacity = 10,
            int maxSize = 100_000)
        {
            _createFunc = createFunc;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;

            #region Invariants

#if UNITY_EDITOR
            if (prewarm > defaultCapacity)
                Debug.LogWarning(
                    $"prewarm=[{prewarm}] should be equal or less than defaultCapacity=[{defaultCapacity}]");
            if (defaultCapacity > maxSize)
                Debug.LogWarning(
                    $"defaultCapacity=[{defaultCapacity}] should be equal or less than maxSize=[{maxSize}]");
#endif

            #endregion

            _maxSize = maxSize;

            _pool = new List<T>(defaultCapacity);
            for (var idx = 0; idx < prewarm; idx++)
            {
                var obj = Get();
                Release(obj);
            }
        }

        public T Get()
        {
            var poolCount = _pool.Count;
            T obj;
            if (poolCount > 0)
            {
                // there are object(s) in the pool
                // use last of them
                obj = _pool[poolCount - 1];
                _pool.RemoveAt(poolCount - 1);
            }
            else
            {
                // there are NO objects in the pool
                // create a new one
                obj = _createFunc();
            }

            _actionOnGet(obj);
            return obj;
        }

        public void Release(T obj)
        {
            var poolCount = _pool.Count;
            _actionOnRelease(obj);
            if (poolCount >= _maxSize)
                _actionOnDestroy(obj);
            else
                _pool.Add(obj);
        }
    }
}

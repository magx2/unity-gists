using System;

namespace Misc
{
    public interface IOptional<T>
    {
        bool IsEmpty();

        bool IsNotEmpty();

        T Value();

        T OrElse(T other);

        IOptional<TOut> Map<TOut>(Func<T, TOut> mapper);

    }

    public class Something<T> : IOptional<T>
    {
        private readonly T _value;

        public static Something<T> Of(T value)
        {
            return new Something<T>(value);
        }
        
        private Something(T value)
        {
            if (value == null) throw new NullReferenceException("Value was null!");
            _value = value;
        }
        
        public bool IsEmpty()
        {
            return false;
        }

        public bool IsNotEmpty()
        {
            return true;
        }

        public T Value()
        {
            return _value;
        }

        public T OrElse(T other)
        {
            return _value;
        }

        public IOptional<TOut> Map<TOut>(Func<T, TOut> mapper)
        {
            return new Something<TOut>(mapper.Invoke(_value));
        }
    }

    public class Nothing<T> : IOptional<T>
    {
        public static Nothing<T> Of()
        {
            return new Nothing<T>();
        }

        private Nothing()
        {
        }
        
        public bool IsEmpty()
        {
            return true;
        }

        public bool IsNotEmpty()
        {
            return false;
        }

        public T Value()
        {
            throw new Exception("There is no value!");
        }

        public T OrElse(T other)
        {
            return other;
        }

        public IOptional<TOut> Map<TOut>(Func<T, TOut> mapper)
        {
            return new Nothing<TOut>();
        }
    }
}

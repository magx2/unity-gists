using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    /// <summary>
    ///     Do not use this in:
    ///     Android (IL2CPP)
    ///     iOS(IL2CPP)
    ///     Standalone (IL2CPP)
    ///     Universal Windows Platform(IL2CPP)
    ///     WebGL(IL2CPP)
    ///     , because Ahead-of-time compile (AOT) is not working
    ///     Source: https://docs.unity3d.com/Manual/ScriptingRestrictions.html
    /// </summary>
    /// <typeparam name="T">Type of optional</typeparam>
    public interface IOptional<T>
    {
        bool IsEmpty();

        bool IsNotEmpty();

        T Value();

        T OrElse(T other);

        IOptional<TOut> Map<TOut>(Func<T, TOut> mapper);

        void IfPresent(Action<T> action);
    }

    public class Something<T> : IOptional<T>
    {
        private readonly T _value;

        private Something(T value) {
            if (value == null) throw new NullReferenceException("Value was null!");
            _value = value;
        }

        public bool IsEmpty() {
            return false;
        }

        public bool IsNotEmpty() {
            return true;
        }

        public T Value() {
            return _value;
        }

        public T OrElse(T other) {
            return _value;
        }

        public IOptional<TOut> Map<TOut>(Func<T, TOut> mapper) {
            return new Something<TOut>(mapper.Invoke(_value));
        }

        public void IfPresent(Action<T> action) {
            action.Invoke(_value);
        }

        public static Something<T> Of(T value) {
            return new Something<T>(value);
        }
    }

    public class Nothing<T> : IOptional<T>
    {
        private Nothing() { }

        public bool IsEmpty() {
            return true;
        }

        public bool IsNotEmpty() {
            return false;
        }

        public T Value() {
            throw new Exception("There is no value!");
        }

        public T OrElse(T other) {
            return other;
        }

        public IOptional<TOut> Map<TOut>(Func<T, TOut> mapper) {
            return new Nothing<TOut>();
        }

        public void IfPresent(Action<T> action) {
            // do nothing
        }

        public static Nothing<T> Of() {
            return new Nothing<T>();
        }
    }

    public class RuntimeOptional<T> : IOptional<T>
    {
        private readonly T _value;

        private RuntimeOptional(T value) {
            _value = value;
        }

        public bool IsEmpty() {
            return _value == null;
        }

        public bool IsNotEmpty() {
            return _value != null;
        }

        public T Value() {
            if (_value == null) throw new NullReferenceException("Value was null!");
            return _value;
        }

        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public T OrElse(T other) {
            if (_value != null)
                return _value;
            else
                return other;
        }

        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        public IOptional<TOut> Map<TOut>(Func<T, TOut> mapper) {
            if (_value != null)
                return new RuntimeOptional<TOut>(mapper.Invoke(_value));
            else
                return Nothing<TOut>.Of();
        }

        public void IfPresent(Action<T> action) {
            if (_value != null) action.Invoke(_value);
        }

        public static RuntimeOptional<T> Of(T value) {
            return new RuntimeOptional<T>(value);
        }
    }

    public class MonoBehaviourOptional<TMono> : IOptional<TMono> where TMono : MonoBehaviour
    {
        private readonly TMono _value;

        private MonoBehaviourOptional(TMono value) {
            _value = value;
        }

        public bool IsEmpty() {
            return _value == null;
        }

        public bool IsNotEmpty() {
            return _value != null;
        }

        public TMono Value() {
            if (_value == null) throw new NullReferenceException("Value was null!");
            return _value;
        }

        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public TMono OrElse(TMono other) {
            if (_value != null)
                return _value;
            else
                return other;
        }

        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        public IOptional<TOut> Map<TOut>(Func<TMono, TOut> mapper) {
            if (_value != null)
                return RuntimeOptional<TOut>.Of(mapper.Invoke(_value));
            else
                return Nothing<TOut>.Of();
        }

        public void IfPresent(Action<TMono> action) {
            if (_value != null) action.Invoke(_value);
        }

        public static MonoBehaviourOptional<TMono> Of(TMono value) {
            return new MonoBehaviourOptional<TMono>(value);
        }
    }
}
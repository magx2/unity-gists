using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc.StateMachine
{
    public readonly struct StateMessage
    {
        private readonly IDictionary<string, object> _dictionary;

        public StateMessage(params object[] objects) {
            if (objects.Length % 2 != 0) throw new Exception($"Size {objects.Length} is not even!");
            _dictionary = new Dictionary<string, object>(objects.Length % 2);
            for (var idx = 0; idx < objects.Length; idx += 2)
                _dictionary[objects[idx].ToString()] = objects[idx + 1];
        }

        public object this[string key] {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public bool GetBool(string key) {
            return (bool) this[key];
        }

        public bool GetBoolOrDefault(string key, bool @default) {
            if (!HasKey(key)) return @default;
            return (bool) this[key];
        }

        public string GetString(string key) {
            return (string) this[key];
        }

        public Vector2 GetVector2(string key) {
            return (Vector2) this[key];
        }

        public T Get<T>(string key) {
            return (T) this[key];
        }

        public bool HasKey(string key) {
            return _dictionary != null && _dictionary.ContainsKey(key);
        }

        public int GetInt(string key) {
            return (int) this[key];
        }

        public float GetFloat(string key) {
            return (float) this[key];
        }
    }
}
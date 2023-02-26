using System.Collections.Generic;

namespace Misc
{
    public class TextParser
    {
        private readonly IDictionary<string, object> _variables = new Dictionary<string, object>();

        public object this[string key] {
            set => _variables[key] = value;
        }

        public string Parse(string text) {
            if (text == null) return null;

            foreach (var variable in _variables) {
                var key = variable.Key;
                var value = variable.Value;
                text = text.Replace($"%{key}", value.ToString());
            }

            return text;
        }
    }
}
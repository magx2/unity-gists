namespace Misc
{
    /// <summary>
    ///     Find your colors here: https://www.google.com/search?client=firefox-b-d&q=color+picker
    /// </summary>
    public static class Colors
    {
        public static IColorScheme currentColorScheme = Bright.Instance;

        public static string ColorText(string color, string text) {
            return $"<color={color}>{text}</color>";
        }

        public static string ColorMe(this object text, string color) {
            return $"<color={color}>{text}</color>";
        }

        public static void SwitchToBrightMode() {
            currentColorScheme = Bright.Instance;
        }

        public static void SwitchToDarkMode() {
            currentColorScheme = Dark.Instance;
        }


        public interface IColorScheme
        {
            string Contrast();
            string Green();
            string Orange();
            string Yellow();
            string Purple();
            string Grey();
            string Red();
        }

        public class Bright : IColorScheme
        {
            public static readonly Bright Instance = new Bright();

            private Bright() { }

            public string Contrast() {
                return "#ffffff";
            }

            public string Green() {
                return "#14ff1f";
            }

            public string Orange() {
                return "#ffc803";
            }

            public string Yellow() {
                return "#fcfc03";
            }

            public string Purple() {
                return "#9178ff";
            }

            public string Grey() {
                return "#c7c7c7";
            }

            public string Red() {
                return "#ff4242";
            }
        }

        public class Dark : IColorScheme
        {
            public static readonly Dark Instance = new Dark();

            private Dark() { }

            public string Contrast() {
                return "#000000";
            }

            public string Green() {
                return "#0c9412";
            }

            public string Orange() {
                return "#a38000";
            }

            public string Yellow() {
                return "#b8b806";
            }

            public string Purple() {
                return "#51438c";
            }

            public string Grey() {
                return "#808080";
            }

            public string Red() {
                return "#a32929";
            }
        }
    }
}
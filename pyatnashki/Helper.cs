using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pyatnashki
{
    public static class Helpers
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Name == (name));
        }

        public static void OpenParameters()
        {
            if (IsWindowOpen<Parameters>()) return;

            Parameters parameters = new Parameters();
            parameters.ShowDialog();
        }

        public static void OpenResults ()
        {
            if (IsWindowOpen<Results>()) return;

            Results results = new Results();
            results.ShowDialog();
        }

        public static T GetWindow<T>(string name = "") where T : Window
        {
            foreach (var result in Application.Current.Windows.OfType<T>())
                return result;

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pyatnashki
{
    internal interface IButtonView
    {
        public Button button { get; set; }

        public void InitializeButton(int row, int column, string name);
    }
}

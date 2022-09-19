using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pyatnashki
{
    internal interface IMovable
    {
        public void Move(int row, int column);
    }
}

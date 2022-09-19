using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyatnashki
{
    public class DataResult
    {
        public DataResult(int index, string dateSave = "", string typeField = "", string timeComplete = "", string count = "")
        {
            this.index = index;
            this.dateSave = dateSave;
            this.typeField = typeField;
            this.timeComplete = timeComplete;
            this.count = count;
        }

        public int index { get; set; }
        public string dateSave { get; set; }
        public string typeField { get; set; }
        public string timeComplete { get; set; }
        public string count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderSlave.Core
{
    public class ExecutionResult
    {
        public TimeSpan ProcessorTime { get; set; }

        public long TotalMemoryAllocated { get; set; }

        public string ConsoleOutput { get; set; }

        public string Result { get; set; }

        public bool IsSucceed { get; set; } = false;
    }
}

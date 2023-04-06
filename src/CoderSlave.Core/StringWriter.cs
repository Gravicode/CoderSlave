using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderSlave.Core
{
    public class StringWriter : TextWriter
    {
        private StringBuilder sb;
        public StringWriter()
        {
            this.sb = new StringBuilder();
        }

        public override void Write(char value)
        {
            sb.Append(value);
        }

        public override void Write(string value)
        {
            sb.Append(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        public string GetContent()
        {
            return sb.ToString();
        }

        public void Reset()
        {
            sb.Clear();
        }
    }
}

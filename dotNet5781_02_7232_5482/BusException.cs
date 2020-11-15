using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{

    class BusException : Exception
    {
        public BusException() : base() { }
        public BusException(string message) : base(message) { }
        public BusException(string message, Exception e) : base(message, e) { }
        protected BusException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

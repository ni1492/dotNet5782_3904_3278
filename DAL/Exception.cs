using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        public class NotFoundException : Exception
        {
            public NotFoundException() : base() { }
            public NotFoundException(string message) : base(message) { }
            public NotFoundException(string message, Exception inner) : base(message, inner) { }
            public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
       
        public class ExistException : Exception
        {
            public ExistException() : base() { }
            public ExistException(string message) : base(message) { }
            public ExistException(string message, Exception inner) : base(message, inner) { }
            public ExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
       
    }